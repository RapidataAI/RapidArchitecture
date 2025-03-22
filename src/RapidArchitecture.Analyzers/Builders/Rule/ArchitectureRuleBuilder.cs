using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class ArchitectureRuleBuilder
{
    public ArchitectureRuleBuilder(IScopeBuilder scope)
    {
        Scope = scope;
    }

    public IScopeBuilder Scope { get; set; }
    
    public IEnumerable<DiagnosticDescriptor> Descriptors => Evaluations.Select(e => e.Descriptor);
    
    private IList<IEvaluationBuilder> Evaluations { get; } = new List<IEvaluationBuilder>();
    
    public ArchitectureRuleBuilder Must(Func<TypeDeclarationSyntax, bool> evaluation)
    {
        var evaluationBuilder = new ExpressionEvaluationBuilder(evaluation);
        Evaluations.Add(evaluationBuilder);
        return this;
    }

    public void Apply(SyntaxNodeAnalysisContext obj)
    {
        var matches = Scope.Identify(obj);
        
        foreach (var match in matches)
        {
            foreach (var evaluation in Evaluations)
            {
                evaluation.Evaluate(obj, match);
            }
        }
    }
}