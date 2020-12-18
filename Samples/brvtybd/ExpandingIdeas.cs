using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.brvtybd
{
    class ExpandingIdeas
    {
        void Example(string input)
        {
while(!int.TryParse(input, out int choice) || choice < 1)
{
    Console.WriteLine("choose a valid option");
    input = Console.ReadLine();
}
        }

        void Example2()
        {
            var userGroupValidatorFactory = default(object);

        }

        object AddCategory(bool condition1, bool condition2)
        {
            object dependency = null;
            object returnValue;

return condition1 ? condition2 ? DoTheThing(dependency) : 
    DoTheThing(default) : default;
        }

        object DoTheThing(object input)
        {
            return default;
        }
        
    }
}
