using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;

namespace RapidArchitecture.Analyzers.Rules;

public class SymbolArchitectureRule<TSymbol> : IArchitectureRule<TSymbol> 
    where TSymbol : class, ISymbol
{
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

    public IList<IEvaluator<TSymbol>> Evaluations { get; } = [];
}