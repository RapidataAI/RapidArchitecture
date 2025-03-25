using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxScope<TSyntaxNode> where TSyntaxNode : SyntaxNode
{
    public SyntaxScope(SyntaxKind[] syntaxKinds, Expression<Func<TSyntaxNode, bool>> filter)
    {
        SyntaxKinds = syntaxKinds;
        Filter = filter.Compile();
    }
    
    public SyntaxKind[] SyntaxKinds { get; }

    private Func<TSyntaxNode, bool> Filter { get; }

    public IEnumerable<TSyntaxNode> Identify(SyntaxNodeAnalysisContext context)
    {
        if(context.Node is TSyntaxNode specificType && Filter(specificType))
        {
            yield return specificType;
        }
    }
}