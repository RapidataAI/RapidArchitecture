using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;

namespace RapidArchitecture.Analyzers.Builders.Locating;

public class FunctionLocator<TSyntaxNode> : ILocator<TSyntaxNode>
{
    public FunctionLocator(Func<TSyntaxNode, Location> expression)
    {
        Locate = expression;
    }

    public Func<TSyntaxNode, Location> Locate { get; }
}