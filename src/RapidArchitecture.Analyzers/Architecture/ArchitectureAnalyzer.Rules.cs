using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Builders.Scope;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Architecture;

public partial class ArchitectureAnalyzer
{
    private readonly IList<IArchitectureRule> _rules = [];

    protected InitialArchitectureRuleBuilder<TSyntaxNode> RuleFor<TSyntaxNode>(IScopeBuilder<TSyntaxNode> scopeBuilder) 
        where TSyntaxNode : SyntaxNode
    {
        var rule = new ArchitectureRule<TSyntaxNode>(scopeBuilder);
        var builder = new InitialArchitectureRuleBuilder<TSyntaxNode>(rule);
        _rules.Add(rule);
        return builder;
    }
}