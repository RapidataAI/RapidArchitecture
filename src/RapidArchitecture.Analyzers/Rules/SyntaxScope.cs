using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxScope<TSyntaxNode> : ISyntaxScope
    where TSyntaxNode : SyntaxNode
{
    public SyntaxScope(SyntaxKind[] syntaxKinds, Func<TSyntaxNode, bool> filter)
    {
        SyntaxKinds = syntaxKinds;
        Filter = filter;
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

    public bool IsMatch(SyntaxNode syntaxNode)
    {
        return syntaxNode is TSyntaxNode specificType && Filter(specificType);
    }
}