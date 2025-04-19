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
        Func<string, bool> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : class, ITypeSymbol
    {
        builder.ArchitectureRule.AddEvaluation(x => expression(NameExpression(x)));
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule);
        
        static string NameExpression(TAnalyze x) => x.Name;
    }
}