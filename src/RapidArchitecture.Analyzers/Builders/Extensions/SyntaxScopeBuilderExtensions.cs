using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class SyntaxScopeBuilderExtensions
{
    public static SymbolScopeBuilder<ITypeSymbol> AssignableTo<TSyntaxNode>(this SyntaxScopeBuilder<TSyntaxNode> scopeBuilder, ITypeSymbol typeSymbol) where TSyntaxNode : TypeDeclarationSyntax
    {
        return new SymbolScopeBuilder<ITypeSymbol>(s => s.AllInterfaces.Any(i =>
                i.Equals(typeSymbol, SymbolEqualityComparer.Default) ||
                i.AllInterfaces.Any(ii => ii.Equals(typeSymbol, SymbolEqualityComparer.Default))),
            scopeBuilder.Build());
    }
}