using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public class ScopeBuilder<TSyntaxNode> : IScopeBuilder<TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    public ScopeBuilder(Expression<Func<TSyntaxNode, bool>> filter, SyntaxKind[] syntaxKinds)
    {
        _filter = filter;
        SyntaxKinds = syntaxKinds;
    }
    
    private readonly Expression<Func<TSyntaxNode, bool>> _filter;

    public SyntaxKind[] SyntaxKinds { get; }
    
    public ScopeBuilder<TSyntaxNode> That(Expression<Func<TSyntaxNode, bool>> filter)
    {
        return new ScopeBuilder<TSyntaxNode>(filter, SyntaxKinds);
    }
    
    public ScopeBuilder<TSyntaxNode> ResidingInNamespace(string namespaceName)
    {
        return That(c => c.Ancestors(true).OfType<BaseNamespaceDeclarationSyntax>().Any(n => n.Name.ToString().StartsWith(namespaceName)));
    }
    
    public IEnumerable<TSyntaxNode> Identify(SyntaxNodeAnalysisContext context)
    {
        if(context.Node is TSyntaxNode specificType && _filter.Compile().Invoke(specificType))
        {
            yield return specificType;
        }
    }
}