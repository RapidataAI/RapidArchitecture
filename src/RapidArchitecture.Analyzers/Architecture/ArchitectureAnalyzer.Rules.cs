using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Builders.Scope;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Architecture;

public partial class ArchitectureAnalyzer
{
    private readonly IList<IArchitectureRule> _rules = [];

    protected InitialArchitectureRuleBuilder<SyntaxArchitectureRule<TSyntaxNode>, TSyntaxNode> RuleFor<TSyntaxNode>(SyntaxScopeBuilder<TSyntaxNode> scopeBuilder) 
        where TSyntaxNode : SyntaxNode
    {
        var rule = new SyntaxArchitectureRule<TSyntaxNode>(scopeBuilder.Build());
        var builder = new InitialArchitectureRuleBuilder<SyntaxArchitectureRule<TSyntaxNode>, TSyntaxNode>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    protected InitialArchitectureRuleBuilder<SymbolArchitectureRule<TSymbol>, TSymbol> RuleFor<TSymbol>(SymbolScopeBuilder<TSymbol> scopeBuilder) 
        where TSymbol : class, ISymbol
    {
        var rule = new SymbolArchitectureRule<TSymbol>(scopeBuilder.Build());
        var builder = new InitialArchitectureRuleBuilder<SymbolArchitectureRule<TSymbol>, TSymbol>(rule);
        _rules.Add(rule);
        return builder;
    }
}