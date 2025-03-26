using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Locating;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public class ExpressionEvaluator<TAnalyze> : IEvaluator<TAnalyze>
{
    public ExpressionEvaluator(Expression<Func<TAnalyze, bool>> evaluation, DiagnosticSeverity severity, ILocator<TAnalyze> locator)
    {
        Locator = locator;
        Evaluation = evaluation.Compile();
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