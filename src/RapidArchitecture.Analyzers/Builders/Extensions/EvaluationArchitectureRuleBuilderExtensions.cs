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
        Func<TAnalyze, bool> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : class
    {
        builder.ArchitectureRule.AddEvaluation(expression);
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule);
    }
    
    public static CompletedArchitectureRuleBuilder<TRule, TAnalyze> HaveNameMatching<TRule, TAnalyze>(
        this EvaluationArchitectureRuleBuilder<TRule, TAnalyze> builder,
        Func<string, bool> expression) 
        where TRule : class, IArchitectureRule<TAnalyze>
        where TAnalyze : TypeDeclarationSyntax
    {
        builder.ArchitectureRule.AddEvaluation(x => expression(NameExpression(x)));
        return new CompletedArchitectureRuleBuilder<TRule, TAnalyze>(builder.ArchitectureRule)
            .WithLocation(x => x.Identifier.GetLocation());
        
        static string NameExpression(TAnalyze x) => x.Identifier.Text;
    }
}