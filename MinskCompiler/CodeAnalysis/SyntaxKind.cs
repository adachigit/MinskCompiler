namespace MinskCompiler.CodeAnalysis
{
    public enum SyntaxKind
    {
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        
        LiteralExpression,
        BinaryExpression,
        ParenthesizedExpression,
    }

}