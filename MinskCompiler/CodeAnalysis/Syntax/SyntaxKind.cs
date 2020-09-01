namespace MinskCompiler.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        PipePipeToken,
        AmpersandAmpersandToken,
        BangToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        IdentifierToken,
        
        // Keywords
        FalseKeyword,
        TrueKeyword,

        // Expressions
        LiteralExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
    }

}