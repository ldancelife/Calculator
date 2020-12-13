using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Lexer
    {
        public List<Token> Tokenize(string input)
        {
            var result = new List<Token>();

            int i = 0;

            while (i < input.Length)
            {
                char c = input[i];

                TokenType tokenType = TokenType.Plus;
                string tokenValue = null;

                if (c == '-')
                {
                    tokenType = TokenType.Minus;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (c == '+')
                {
                    tokenType = TokenType.Plus;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (c == '*')
                {
                    tokenType = TokenType.Mult;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (c == '/')
                {
                    tokenType = TokenType.Div;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (c == '(')
                {
                    tokenType = TokenType.LParen;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (c == ')')
                {
                    tokenType = TokenType.RParen;
                    tokenValue = c.ToString();
                    i++;
                }
                else if (char.IsDigit(c))
                {
                    int j = i;
                    while (j < input.Length && char.IsDigit(input[j]))
                    {
                        j++;
                    }

                    if (j < input.Length && (input[j] == '.'|| input[j] == ','))
                    {
                        j++;
                    }
                    
                    while (j < input.Length && char.IsDigit(input[j]))
                    {
                        j++;
                    }

                    tokenType = TokenType.Number;
                    tokenValue = input.Substring(i, j - i);

                    i = j;
                }
                else if (char.IsWhiteSpace(c))
                {
                    int j = i;
                    while (j < input.Length && char.IsWhiteSpace(input[j]))
                    {
                        j++;
                    }

                    i = j;
                }
                else
                {
                    Console.WriteLine($"Char {c} is not recognized");
                    i++;
                }

                if (tokenValue != null)
                {
                    result.Add(new Token(tokenType, tokenValue));
                }
            }

            return result;
        }
    }
}