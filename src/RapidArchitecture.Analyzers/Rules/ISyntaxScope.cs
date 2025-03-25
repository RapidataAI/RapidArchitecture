using Microsoft.CodeAnalysis;

namespace RapidArchitecture.Analyzers.Rules;

public interface ISyntaxScope
{
    bool IsMatch(SyntaxNode syntaxNode);
}