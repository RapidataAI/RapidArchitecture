using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class EvaluationArchitectureRuleBuilder<TSyntaxNode> 
    where TSyntaxNode : SyntaxNode
{
    protected readonly ArchitectureRule<TSyntaxNode> ArchitectureRule;
    public EvaluationArchitectureRuleBuilder(ArchitectureRule<TSyntaxNode> architectureRule)
    {
        ArchitectureRule = architectureRule;
    }
    
    public CompletedArchitectureRuleBuilder<TSyntaxNode> Custom(Expression<Func<TSyntaxNode, bool>> expression)
    {
        ArchitectureRule.AddEvaluation(new ExpressionEvaluationBuilder<TSyntaxNode>(expression, null, ArchitectureRule.Severity));
        return new CompletedArchitectureRuleBuilder<TSyntaxNode>(ArchitectureRule);
    }
}