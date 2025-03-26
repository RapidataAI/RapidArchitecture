using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Locating;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Extensions;

public static class CompletedArchitectureRuleBuilderExtensions
{
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> WithLocation<TRule, TAnalyze>(
        this CompletedArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Expression<Func<TAnalyze, Location>> location) 
            where TRule : class, IArchitectureRule<TAnalyze> 
            where TAnalyze : class
    {
        var evaluationBuilder = builder.ArchitectureRule.Evaluations.Last()!;
        evaluationBuilder.Locator = new ExpressionLocator<TAnalyze>(location);
        return builder;
    }

    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> WithMessage<TRule, TAnalyze>(
        this CompletedArchitectureRuleBuilder<TRule, TAnalyze> builder,
        string message) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : class 
    {
        var rule = builder.ArchitectureRule.Evaluations.Last()!;
        rule.Descriptor = new DiagnosticDescriptor(rule.Descriptor.Id, rule.Descriptor.Title.ToString(), message,
            rule.Descriptor.Category, rule.Descriptor.DefaultSeverity, rule.Descriptor.IsEnabledByDefault);
        return builder;
    }
}