using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> HaveNameMatching<TRule, TAnalyze>(
        this EvaluationArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Expression<Func<string, bool>> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : TypeDeclarationSyntax
    {
        Expression<Func<TAnalyze, string>> nameExpression = static x => x.Identifier.Text;
        builder.ArchitectureRule.AddEvaluation(x => expression.Compile().Invoke(nameExpression.Compile().Invoke(x)));
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule)
            .WithLocation(x => x.Identifier.GetLocation());
    }
}