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
    protected ArchitectureRule<TSyntaxNode> _architectureRule;
    public EvaluationArchitectureRuleBuilder(ArchitectureRule<TSyntaxNode> architectureRule)
    {
        _architectureRule = architectureRule;
    }
    
    public CompletedArchitectureRuleBuilder<TSyntaxNode> Custom(Expression<Func<TSyntaxNode, bool>> expression)
    {
        _architectureRule.AddEvaluation(new ExpressionEvaluationBuilder<TSyntaxNode>(expression, null));
        return new CompletedArchitectureRuleBuilder<TSyntaxNode>(_architectureRule);
    }
}