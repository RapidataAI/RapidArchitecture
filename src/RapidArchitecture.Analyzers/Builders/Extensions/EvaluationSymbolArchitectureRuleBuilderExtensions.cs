using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class EvaluationSymbolArchitectureRuleBuilderExtensions
{
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> HaveNameMatching<TRule, TAnalyze>(
        this EvaluationArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Expression<Func<string, bool>> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : class, ITypeSymbol
    {
        Expression<Func<TAnalyze, string>> nameExpression = static x => x.Name;
        builder.ArchitectureRule.AddEvaluation(x => expression.Compile().Invoke(nameExpression.Compile().Invoke(x)));
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule);
    }
}