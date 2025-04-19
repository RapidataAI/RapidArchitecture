using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Locating;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public class FunctionEvaluator<TAnalyze> : IEvaluator<TAnalyze>
{
    public FunctionEvaluator(Func<TAnalyze, bool> evaluation, DiagnosticSeverity severity, ILocator<TAnalyze> locator)
    {
        Locator = locator;
        Evaluation = evaluation;
        Descriptor = new DiagnosticDescriptor("RA0001", "Title", "Message", "Category", severity, true);
    }

    private Func<TAnalyze,bool> Evaluation { get; }
    
    public DiagnosticDescriptor Descriptor { get; set; }

    public IEnumerable<Diagnostic> Evaluate(TAnalyze match)
    {
        if (!Evaluation(match))
        {
            yield return Diagnostic.Create(Descriptor, Locator.Locate(match));
        }
    }

    public ILocator<TAnalyze> Locator { get; set; }
}