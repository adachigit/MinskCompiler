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
        EqualsToken,
        EqualsEqualsToken,
        BangEqualsToken,
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
        NameExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
        AssignmentExpression,
    }

}