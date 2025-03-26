using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Locating;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public interface IEvaluator<TAnalyse> : IEvaluator
{
    IEnumerable<Diagnostic> Evaluate(TAnalyse match);
    
    ILocator<TAnalyse> Locator { get; set; }
}

public interface IEvaluator
{
    DiagnosticDescriptor Descriptor { get; set; }
}