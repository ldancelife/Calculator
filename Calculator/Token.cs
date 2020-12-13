﻿namespace Calculator
{
    public class Token
    {
        public TokenType TokenType;

        public string Value;

        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }
}