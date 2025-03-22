using System.Collections.Generic;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Rules;

public class ArchitectureRule
{
    public ArchitectureRule(IScopeBuilder scope)
    {
        Scope = scope;
    }

    public IScopeBuilder Scope { get; set; }
    
    private IList<IEvaluationBuilder> Evaluations { get; } = new List<IEvaluationBuilder>();

    public void Analyze(SyntaxNodeAnalysisContext context)
    {
        foreach (var match in Scope.Identify(context))
        {
            foreach (var evaluation in Evaluations)
            {
                evaluation.Evaluate(context, match);
            }
        }
    }
}