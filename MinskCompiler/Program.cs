using System.Linq;
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
            var variables = new Dictionary<VariableSymbol, object>();

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
                var compliation = new Compilation(syntaxTree);
                var result = compliation.Evaluate(variables);

                var diagnostics = result.Diagnostics;

                if(showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    syntaxTree.Root.WriteTo(Console.Out);
                    Console.ResetColor();
                }

                if (!diagnostics.Any())
                {
                    Console.WriteLine(result.Value);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostic in diagnostics)
                    {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();

                        var prefix = line.Substring(0, diagnostic.Span.Start);
                        var error = line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = line.Substring(diagnostic.Span.End);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.Write(suffix);

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
        }
    }

}