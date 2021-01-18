using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Samples.Reflection
{
public interface ICsvGenerator
{
    IEnumerable<string> OutputToCsv<T>(IEnumerable<T> collection, char delimiter = ',');
}

    public class CsvGeneratorSlow : ICsvGenerator
    {
        public IEnumerable<string> OutputToCsv<T>(
            IEnumerable<T> collection, char delimiter = ',')
        {
            var properties = typeof(T).GetProperties(
                BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead).ToArray();

            yield return properties.Select(p => EscapeAndWrap(p.Name))
                    .Aggregate((p1, p2) => $"{p1}{delimiter}{p2}");

            foreach (var item in collection)
            {
                yield return
                    properties.Select(p => EscapeAndWrap(GetValue(p, item)))
                    .Aggregate((p1, p2) => $"{p1}{delimiter}{p2}");
            }
        }

        private string GetValue<T>(PropertyInfo prop, T obj)
        {
            try
            {
                var val = prop.GetValue(obj);
                return val == null ? "null" : val.ToString();
            }
            catch (Exception)
            {
                return "error";
            }
        }

        private static string EscapeAndWrap(string input)
        {
            return input;
            return $"\"{input.Replace("\"", "\"\"")}\"";
        }

    }

public class CsvGeneratorFast : ICsvGenerator
{
    static MethodInfo _toString;
    static MethodInfo _aggregate;

    static CsvGeneratorFast()
    {
        _toString = typeof(object).GetMethod("ToString");
        _aggregate = typeof(Enumerable).GetMethods()
            .First(m => m.Name == nameof(Enumerable.Aggregate) && m.GetGenericArguments().Length == 1).MakeGenericMethod(typeof(string));
    }

    public IEnumerable<string> OutputToCsv<T>(IEnumerable<T> objects, char delimiter = ',')
    {
        return CsvBuilder<T>.Actual(objects, delimiter);
    }

    private delegate string LineBuilder<T>(char delimiter, T item);
    private delegate string HeaderBuilder(char delimiter);

    private static class CsvBuilder<T>
    {
        static LineBuilder<T> _lineBuilder;
        static HeaderBuilder _headerBuilder = null;

        static CsvBuilder()
        {
            var properties = typeof(T).GetProperties(
                BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead).ToArray();

            if (!properties.Any())
            {
                return;
            }

            _headerBuilder = delimiter =>
                properties.Select(p => EscapeAndWrap(p.Name))
                .Aggregate((s1, s2) => $"{s1}{delimiter}{s2}");

            var valueGetters =
                properties.Select(prop => BuildPropertyGetter(prop)).ToArray();

            _lineBuilder = (delimiter, item) =>
                valueGetters.Select(getter =>
                {
                    try
                    {
                        return getter(item);
                    }
                    catch (Exception)
                    {
                        return "error";
                    }
                })
                .Aggregate((s1, s2) => $"{s1}{delimiter}{s2}");
        }

        public static IEnumerable<string> Actual(IEnumerable<T> items, char delimiter)
        {
            yield return _headerBuilder(delimiter);
            foreach (var item in items)
            {
                yield return _lineBuilder(delimiter, item);
            }
        }

        private static Func<T, string> BuildPropertyGetter(PropertyInfo prop)
        {
            // we're going to build an expression 
            // if the property is a value type
            //     item => item.Property.ToString()
            // otherwise
            //     item => item.Property == null ? "null" : item.Property.ToString();
            var type = typeof(T);
            var input = Expression.Parameter(typeof(T), "item");

            var propExpression = Expression.Property(input, prop);

            var toString = prop.PropertyType != typeof(string) 
                ? (Expression)Expression.Call(propExpression, _toString)
                : propExpression;
                

            if (prop.PropertyType.IsValueType)
            {
                var lambda = Expression.Lambda<Func<T, string>>(toString, input).Compile();
                return item => EscapeAndWrap(lambda(item));
            }
            else
            {
                var nullExpression = Expression.Constant(null);
                var isNull = Expression.Equal(propExpression, nullExpression);
                var condition = Expression.Condition(isNull, Expression.Constant("null"), toString);
                    
                var lambda = Expression.Lambda<Func<T, string>>(condition, input).Compile();
                return item => EscapeAndWrap(lambda(item));
            }
        }

        private static string EscapeAndWrap(string input)
        {
                return input;
            return $"\"{input.Replace("\"", "\"\"")}\"";
        }
    }
}
}
