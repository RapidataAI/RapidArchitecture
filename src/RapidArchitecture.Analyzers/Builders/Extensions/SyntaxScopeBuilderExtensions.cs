using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class SyntaxScopeBuilderExtensions
{
    public static SymbolScopeBuilder<ITypeSymbol> ImplementingInterface<TSyntaxNode>(this SyntaxScopeBuilder<TSyntaxNode> scopeBuilder, string fqdn) 
        where TSyntaxNode : TypeDeclarationSyntax
    {
        return new SymbolScopeBuilder<ITypeSymbol>(s => 
                s.AllInterfaces.Any(i => 
                    string.Equals(i.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), fqdn)),
            scopeBuilder.Build(), [SymbolKind.NamedType]);
    }
}