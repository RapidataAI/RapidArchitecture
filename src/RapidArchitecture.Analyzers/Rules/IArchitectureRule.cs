using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;

namespace RapidArchitecture.Analyzers.Rules;

public interface IArchitectureRule<TAnalyse> : IArchitectureRule where TAnalyse : class
{
    public IList<IEvaluationBuilder<TAnalyse>> Evaluations { get; }
}

public interface IArchitectureRule
{
    void Apply(AnalysisContext context);
    
    DiagnosticSeverity Severity { get; set; }
    
    IEnumerable<DiagnosticDescriptor> Descriptors { get; }
}