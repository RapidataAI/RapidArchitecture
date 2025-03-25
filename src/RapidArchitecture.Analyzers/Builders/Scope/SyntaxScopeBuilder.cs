using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public class SyntaxScopeBuilder<TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    public SyntaxScopeBuilder(Expression<Func<TSyntaxNode, bool>> filter, SyntaxKind[] syntaxKinds)
    {
        _filter = filter;
        SyntaxKinds = syntaxKinds;
    }
    
    private readonly Expression<Func<TSyntaxNode, bool>> _filter;

    public SyntaxKind[] SyntaxKinds { get; }
    
    public SyntaxScopeBuilder<TSyntaxNode> That(Expression<Func<TSyntaxNode, bool>> filter)
    {
        return new SyntaxScopeBuilder<TSyntaxNode>(filter, SyntaxKinds);
    }
    
    public SyntaxScopeBuilder<TSyntaxNode> ResidingInNamespace(string namespaceName)
    {
        return That(c => c.Ancestors(true).OfType<BaseNamespaceDeclarationSyntax>().Any(n => n.Name.ToString().StartsWith(namespaceName)));
    }
    public SyntaxScope<TSyntaxNode> Build()
    {
        return new SyntaxScope<TSyntaxNode>(SyntaxKinds, _filter);
    }
}