﻿using System.Linq;
using System.Collections.Generic;
using System;
using MinskCompiler.CodeAnalysis;
using MinskCompiler.CodeAnalysis.Syntax;
using MinskCompiler.CodeAnalysis.Binding;

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
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);

                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

                if(showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!syntaxTree.Diagnostics.Any())
                {
                    var evaluator = new Evaluator(boundExpression);
                    Console.WriteLine($"Result is {evaluator.Evaluate()}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);

                    Console.ResetColor();
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