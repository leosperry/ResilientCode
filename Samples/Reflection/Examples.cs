using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Samples.Reflection
{
    public class Examples
    {

void OkReflectionExamples<T>(MyType someObject)
{
    // getting a referene to a Type
    Type t = typeof(T);
    t = someObject.GetType();

    // getting member information
    PropertyInfo propInfo = t.GetProperty(nameof(MyType.MyProperty)
        , BindingFlags.Public | BindingFlags.Instance);

    MethodInfo methodInfo = t.GetMethod(
        nameof(MyType.MyMethod), BindingFlags.DeclaredOnly);

    //dicovering attributes
    var attribute = t.GetCustomAttribute<MyAttribute>();

    //discovering if a type is derived from or implements another
    bool inherits = typeof(object).IsAssignableFrom(t);
}

void ExpensiveReflectionExamples(object someObject,
    PropertyInfo someProperty, MethodInfo someMethod)
{
    someProperty.GetValue(someObject);
    someMethod.Invoke(someObject, null);
}

        public class MyAttribute : Attribute
        {

        }

        public class MyType : MyInterface
        {
            public int MyProperty { get; set; }
            public void MyMethod() { }
        }

        public interface MyInterface
        {
            public int MyProperty { get; set; }
        }
    }
}
