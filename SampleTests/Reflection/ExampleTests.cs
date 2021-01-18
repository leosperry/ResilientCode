using Samples.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SampleTests.Reflection
{
    
    public class ExampleTests
    {
        static ExampleTests()
        {
            //initialize prior to test
            //var type = typeof(CsvGeneratorFast.CsvBuilder<TestModel>);
            //type.GetConstructors();
        }
        public ExampleTests()
        {
        }

        [Fact]
        public void TestSlow()
        {
            CsvGeneratorSlow generator = new CsvGeneratorSlow();

            var results = generator.OutputToCsv(GetTestData(), '|').ToArray();
        }
        [Fact]
        public void TestSlow2()
        {
            CsvGeneratorSlow generator = new CsvGeneratorSlow();

            var results = generator.OutputToCsv(GetTestData(), '|').ToArray();
        }

        [Fact]
        public void TestFast()
        {
            CsvGeneratorFast generator = new CsvGeneratorFast();

            var results = generator.OutputToCsv(GetTestData(), '|').ToArray();
        }

        [Fact]
        public void TestFast2()
        {
            CsvGeneratorFast generator = new CsvGeneratorFast();

            var results = generator.OutputToCsv(GetTestData(), '|').ToArray();
        }

        //[Fact]
        //public void TestWithException()
        //{
        //    CsvGeneratorFast generator = new CsvGeneratorFast();

        //    var results = generator.OutputToCsv(new ExceptionModel[] { new ExceptionModel()}).ToArray();
        //}

        private IEnumerable<TestModel> GetTestData()
        {
            const int count = 5000000;
            for (int i = 0; i < count; i++)
            {
                yield return new TestModel(i, "Enterprise\"D")
                {
                    Captain = "James Kirk",
                    Class = "Constitution"
                };
            }
        }
    }

    class TestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime LaunchDate { get; set; }
        public string Captain { get; set; }
        
        //public string Captain2 { get; set; }
        //public string Captain3 { get; set; }
        //public string Captain4 { get; set; }
        //public string Captain5 { get; set; }
        //public string Captain6 { get; set; }
        //public string Captain7 { get; set; }
        //public string Captain8 { get; set; }
        //public string Captain9 { get; set; }
        //public string Captain0 { get; set; } = "Jean Luc Picard";
        //public string Captain01 { get; set; } = "Jean Luc Picard";
        //public string Captain02 { get; set; } = "Jean Luc Picard";
        //public string Captain03 { get; set; } = "Jean Luc Picard";
        //public string Captain04 { get; set; } = "Jean Luc Picard";
        //public string Captain05 { get; set; } = "Jean Luc Picard";
        //public string Captain06 { get; set; } = "Jean Luc Picard";
        //public string Captain07 { get; set; } = "Jean Luc Picard";
        //public string Captain08 { get; set; } = "Jean Luc Picard";
        //public string Captain09 { get; set; } = "Jean Luc Picard";

        public string Class { get; set; }

        public TestModel(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public TestModel Custom { get { return new TestModel(1234,""); } }

        public override string ToString()
        {
            return "Custom";
        }
    }

    public class ExceptionModel
    {
        public int MyProperty 
        { 
            get
            {
                throw new Exception();
            }
        }
    }
}
