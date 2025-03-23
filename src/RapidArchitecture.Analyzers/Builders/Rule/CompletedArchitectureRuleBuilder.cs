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
    
    public CompletedArchitectureRuleBuilder<TSyntaxNode> WithLocation(Expression<Func<TSyntaxNode, Location>> location)
    {
        var rule = ArchitectureRule.Evaluations.Last()!;
        rule.GetLocation = location;
        return this;
    }
    
    public CompletedArchitectureRuleBuilder<TSyntaxNode> WithMessage(string message)
    {
        var rule = ArchitectureRule.Evaluations.Last()!;
        rule.Descriptor = new DiagnosticDescriptor(rule.Descriptor.Id, rule.Descriptor.Title.ToString(), message, rule.Descriptor.Category, rule.Descriptor.DefaultSeverity, rule.Descriptor.IsEnabledByDefault);
        return this;
    }
}