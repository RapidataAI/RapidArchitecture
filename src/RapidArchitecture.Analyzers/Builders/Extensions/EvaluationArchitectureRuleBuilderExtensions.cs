using System;
using System.Linq.Expressions;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class EvaluationArchitectureRuleBuilderExtensions
{
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> Custom<TRule, TAnalyze>(
        this EvaluationArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Expression<Func<TAnalyze, bool>> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : class
    {
        builder.ArchitectureRule.AddEvaluation(expression);
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule);
    }
}