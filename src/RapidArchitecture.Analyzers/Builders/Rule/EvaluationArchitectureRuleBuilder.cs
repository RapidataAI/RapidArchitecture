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
    protected readonly SyntaxArchitectureRule<TSyntaxNode> SyntaxArchitectureRule;
    public EvaluationArchitectureRuleBuilder(SyntaxArchitectureRule<TSyntaxNode> syntaxArchitectureRule)
    {
        SyntaxArchitectureRule = syntaxArchitectureRule;
    }
    
    public CompletedArchitectureRuleBuilder<TSyntaxNode> Custom(Expression<Func<TSyntaxNode, bool>> expression)
    {
        SyntaxArchitectureRule.AddEvaluation(new ExpressionEvaluationBuilder<TSyntaxNode>(expression, null, SyntaxArchitectureRule.Severity));
        return new CompletedArchitectureRuleBuilder<TSyntaxNode>(SyntaxArchitectureRule);
    }
}