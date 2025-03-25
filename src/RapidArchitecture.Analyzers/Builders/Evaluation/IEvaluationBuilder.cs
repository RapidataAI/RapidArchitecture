using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public interface IEvaluationBuilder<TAnalyse> : IEvaluationBuilder
{
    void Evaluate(SyntaxNodeAnalysisContext context, TAnalyse match);
    
    Expression<Func<TAnalyse, Location>> GetLocation { get; set; }
}

public interface IEvaluationBuilder
{
    DiagnosticDescriptor Descriptor { get; set; }
}