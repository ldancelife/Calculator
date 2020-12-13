using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _tokenIndex;
        private int _depth;

        public double Parse(List<Token> tokens)
        {
            _tokens = tokens;
            return PlusMinus();
        }

        private double PlusMinus()
        {
            // add
            //    : MultDiv ((Plus | Minus) MultDiv)*
            //    ;

            _depth++;
            double result = MultDiv();

            while (true)
            {
                if (!Accept(TokenType.Plus, out Token operatorToken))
                {
                    if (!Accept(TokenType.Minus, out operatorToken))
                    {
                        if (_depth == 0 && _tokenIndex < _tokens.Count)
                        {
                            Console.WriteLine($"Tokens aren't read to the end of line at {_tokens[_tokenIndex].Value}");
                        }

                        _depth--;
                        return result;
                    }
                }

                double right = MultDiv();
                if (operatorToken.TokenType == TokenType.Plus)
                    result += right;
                else
                    result -= right;
            }
        }

        private double MultDiv()
        {
            // MultDiv
            //    : Expr ((Mult | Div) Expr)*
            //    ;

            _depth++;
            double result = UnaryExpr();

            while (true)
            {
                if (!Accept(TokenType.Mult, out Token opToken))
                {
                    if (!Accept(TokenType.Div, out opToken))
                    {
                        _depth--;
                        return result;
                    }
                }

                var right = UnaryExpr();
                if (opToken.TokenType == TokenType.Mult)
                    result *= right;
                else
                    result /= right;
            }
        }

        private double UnaryExpr()
        {
            _depth++;
            // 
            if (!Accept(TokenType.Plus, out Token opToken))
            {
                Accept(TokenType.Minus, out opToken);
            }

            var result = Expr();
            
            if (opToken!=null)
            {
                if (opToken.TokenType == TokenType.Minus)
                    result = -result;
                
            }

            _depth--;
            return result;
        }

        private double Expr()
        {
            // Expr
            //     : LParen PlusMinus RParen
            //     | NUMBER
            //     ;

            _depth++;
            if (Accept(TokenType.LParen, out _))
            {
                var result = PlusMinus();
                Expect(TokenType.RParen, out _);
                _depth--;
                return result;
            }

            if (Expect(TokenType.Number, out Token numberToken))
            {
                _depth--;
                return double.Parse(numberToken.Value.Replace(',','.'));
            }

            _depth--;
            return 0;
        }

        private bool Expect(TokenType tokenType, out Token token)
        {
            if (Accept(tokenType, out token))
            {
                return true;
            }

            Console.WriteLine($"Token {tokenType} is expected");
            return false;
        }

        private bool Accept(TokenType tokenType, out Token token)
        {
            token = null;

            if (_tokenIndex >= _tokens.Count)
            {
                return false;
            }

            if (_tokens[_tokenIndex].TokenType == tokenType)
            {
                token = _tokens[_tokenIndex];
                _tokenIndex++;
                return true;
            }

            return false;
        }
    }
}