using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    class Token
    {
        string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        TokenType tokenType;

        public TokenType TokenType
        {
            get { return tokenType; }
            set { tokenType = value; }
        }

        Func<string, bool> expr;

        public Func<string, bool> Expr
        {
            get { return expr; }
            set { expr = value; }
        }
    }

    public enum TokenType
    {
        Word, 
        AndOperator, 
        OrOperator, 
        LeftParenthesis, 
        RightParenthesis, 
        Empty, 
        Expr
    }
}
