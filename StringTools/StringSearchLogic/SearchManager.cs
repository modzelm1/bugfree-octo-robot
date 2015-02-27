using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    /// <summary>
    /// Class for client
    /// </summary>
    public class SearchManager
    {
        Parser parser;
        SimpleLexicalAnalyzer simpleLexicalAnalizer;
        QueryTreeBuilder queryTreeBuilder;

        public SearchManager()
        {
            parser = new Parser();
            simpleLexicalAnalizer = new SimpleLexicalAnalyzer();
            queryTreeBuilder = new QueryTreeBuilder();
        }

        public Func<string, bool> BuildStringQueryFunction(string query)
        {
            QueryProcessor processor = new QueryProcessor();
            var listOfTokens = parser.Tokenize(query);
            simpleLexicalAnalizer.AnalizeTokens(listOfTokens);
            var res = processor.ProcessTokenList(listOfTokens);

            return res;
        }
    }
}
