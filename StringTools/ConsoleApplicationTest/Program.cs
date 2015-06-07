using StringSearchLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var query = "small AND cat";
            var query = "small OR cat";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);

            //using generated function fun for filtering list
            foreach (var item in GetTestData().Where(x=>fun(x.description)))
            {
                Console.WriteLine("Pet: {0}, {1}", item.name, item.description);
            }
            Console.ReadKey();
        }

        static List<TestData> GetTestData()
        {
            return new List<TestData>() 
            {
                new TestData(){ name = "dog 1", description="big black dog" },
                new TestData(){ name = "dog 2", description="big white dog" },
                new TestData(){ name = "dog 3", description="small black dog" },
                new TestData(){ name = "cat 1", description="striped cat" },
                new TestData(){ name = "cat 2", description="black and white cat" },
                new TestData(){ name = "cat 3", description="small black cat" }
            };
        }
    }

    class TestData
    {
        public string name { get; set; }
        public string description { get; set; }
    }

}
