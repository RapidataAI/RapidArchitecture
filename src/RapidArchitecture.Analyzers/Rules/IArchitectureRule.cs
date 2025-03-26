using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;

namespace RapidArchitecture.Analyzers.Rules;

public interface IArchitectureRule<TAnalyse> : IArchitectureRule where TAnalyse : class
{
    public IReadOnlyList<IEvaluator<TAnalyse>> Evaluations { get; }
    
    public void AddEvaluation(Expression<Func<TAnalyse, bool>> evaluation);
}

public interface IArchitectureRule
{
    void Apply(AnalysisContext context);
    
    DiagnosticSeverity Severity { get; set; }
    
    IEnumerable<DiagnosticDescriptor> Descriptors { get; }
}