using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;

namespace RapidArchitecture.Analyzers.Builders.Locating;

public class ExpressionLocator<TSyntaxNode> : ILocator<TSyntaxNode>
{
    public ExpressionLocator(Expression<Func<TSyntaxNode, Location>> expression)
    {
        Locate = expression.Compile();
    }

    public Func<TSyntaxNode, Location> Locate { get; }
}