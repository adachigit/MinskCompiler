using System.Linq;
using System.Collections.Generic;
using System;
using MinskCompiler.CodeAnalysis;
using MinskCompiler.CodeAnalysis.Syntax;

namespace MinskCompiler
{
    internal static class Program
    {
        private static void Main()
        {
            var showTree = false;

            while(true)
            {
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                if(line.Equals("#showTree"))
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                }
                else if(line.Equals("#cls"))
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if(showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if(syntaxTree.Diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach(var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);

                    Console.ResetColor();
                }
                else
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    Console.WriteLine($"Result is {evaluator.Evaluate()}");
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if(node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach(var child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }

}