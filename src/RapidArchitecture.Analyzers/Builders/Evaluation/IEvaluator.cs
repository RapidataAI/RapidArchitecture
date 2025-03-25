using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public interface IEvaluator<TAnalyse> : IEvaluator
{
    IEnumerable<Diagnostic> Evaluate(TAnalyse match);
    
    Expression<Func<TAnalyse, Location>> GetLocation { get; set; }
}

public interface IEvaluator
{
    DiagnosticDescriptor Descriptor { get; set; }
}