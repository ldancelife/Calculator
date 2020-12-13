using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string input = "-28.464646 + -2 * +2";
            var lexer = new Lexer();

            Console.WriteLine("Tokens:");
            var tokens = lexer.Tokenize(input);
            foreach (var token in tokens)
                Console.WriteLine($"Type: {token.TokenType}; Value: {token.Value}");

            var parser = new Parser();
            Console.WriteLine();
            Console.WriteLine($"Result: {parser.Parse(tokens):0.00000}");
        }
    }
}