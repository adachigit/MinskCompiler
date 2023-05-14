using MinskCompiler.CodeAnalysis.Syntax;

namespace Minsk.Tests.CodeAnalysis.Syntax;

internal sealed class AssertingEnumerator : IDisposable
{
    private readonly IEnumerator<SyntaxNode> _enumerator;

    public AssertingEnumerator(SyntaxNode node)
    {
        _enumerator = Flatten(node).GetEnumerator();
    }

    public void Dispose()
    {
        Assert.False(_enumerator.MoveNext());
        _enumerator.Dispose();
    }

    private static IEnumerable<SyntaxNode> Flatten(SyntaxNode node)
    {
        var stack = new Stack<SyntaxNode>();
        stack.Push(node);

        while (stack.Count > 0)
        {
            var n = stack.Pop();
            yield return n;

            foreach (var child in n.GetChildren().Reverse())
                stack.Push(child);
        }
    }

    public void AssertNode(SyntaxKind kind)
    {
        try
        {
            Assert.True(_enumerator.MoveNext());
            Assert.IsNotType<SyntaxToken>(_enumerator.Current);
            Assert.Equal(kind, _enumerator.Current.Kind);
        }
        catch (Exception)
        {
            throw new Exception($"Unexpected node {_enumerator.Current.Kind} instead of {kind}");
        }
    }

    public void AssertToken(SyntaxKind kind, string text)
    {
        try
        {
            Assert.True(_enumerator.MoveNext());
            var token = Assert.IsType<SyntaxToken>(_enumerator.Current);
            Assert.Equal(kind, token.Kind);
            Assert.Equal(text, token.Text);
        }
        catch (Exception)
        {
            throw new Exception($"Unexpected token {_enumerator.Current.Kind} instead of {kind}");
        }
    }
}
