using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    class ComparerFunctionsFactory
    {
        /// <summary>
        /// Creates Contains Comparer
        /// </summary>
        /// <param name="lookingFor"></param>
        /// <returns></returns>
        public static Func<string, bool> CreateContainsComparer(string lookingFor)
        {
            return x => x.ToLower().Contains(lookingFor.ToLower());
        }

        /// <summary>
        /// Creates Equals Comparer
        /// </summary>
        /// <param name="lookingFor"></param>
        /// <returns></returns>
        public static Func<string, bool> CreateEqualsComparer(string lookingFor)
        {
            return x => x.ToLower().Equals(lookingFor.ToLower());
        }

        /// <summary>
        /// Combines two functions with AND
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static Func<string, bool> ComparersUnion(Func<string, bool> f1, Func<string, bool> f2)
        {
            return x => f1(x) && f2(x);
        }

        /// <summary>
        /// Combines two functions with OR
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static Func<string, bool> ComparersIntersection(Func<string, bool> f1, Func<string, bool> f2)
        {
            return x => f1(x) || f2(x);
        }
    }
}
