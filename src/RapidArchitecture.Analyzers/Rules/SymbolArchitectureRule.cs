using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Locating;

namespace RapidArchitecture.Analyzers.Rules;

public class SymbolArchitectureRule<TSymbol> : IArchitectureRule<TSymbol> 
    where TSymbol : class, ISymbol
{
    private readonly List<IEvaluator<TSymbol>> _evaluations = [];
    private SymbolScope<TSymbol> Scope { get; }

    public SymbolArchitectureRule(SymbolScope<TSymbol> scope)
    {
        Scope = scope;
    }

    public void Apply(AnalysisContext context)
    {
        context.RegisterSymbolAction(Analyze, Scope.SymbolKinds);
    }

    private void Analyze(SymbolAnalysisContext context)
    {
        var matches = Scope.Identify(context);

        foreach (var match in matches)
        {
            foreach (var evaluation in Evaluations)
            {
                foreach (var diagnostic in evaluation.Evaluate(match))
                {
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    public DiagnosticSeverity Severity { get; set; }
    
    public IEnumerable<DiagnosticDescriptor> Descriptors => Evaluations.Select(x => x.Descriptor);

    public IReadOnlyList<IEvaluator<TSymbol>> Evaluations => _evaluations.AsReadOnly();
    public void AddEvaluation(Expression<Func<TSymbol, bool>> evaluation)
    {
        _evaluations.Add(new ExpressionEvaluator<TSymbol>(evaluation, Severity, new ExpressionLocator<TSymbol>(e => e.Locations.First())));
    }
}