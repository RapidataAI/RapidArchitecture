using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class EvaluationArchitectureRuleBuilderExtensions
{
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> Custom<TRule, TAnalyze>(
        this EvaluationArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Expression<Func<TAnalyze, bool>> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : SyntaxNode 
    {
        builder.ArchitectureRule.Evaluations.Add(
            new ExpressionEvaluationBuilder<TAnalyze>(expression, null, builder.ArchitectureRule.Severity));
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule);
    }
}