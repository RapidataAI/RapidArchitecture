using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class CompletedArchitectureRuleBuilder<TSyntaxNode> : EvaluationArchitectureRuleBuilder<TSyntaxNode> 
    where TSyntaxNode : SyntaxNode
{
    public CompletedArchitectureRuleBuilder(ArchitectureRule<TSyntaxNode> architectureRule) : base(architectureRule)
    {
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> WithLocation(Expression<Func<TSyntaxNode, Location>> location)
    {
        var rule = _architectureRule.Evaluations.Last()!;
        rule.GetLocation = location;
        return this;
    }
}