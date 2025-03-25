using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxArchitectureRule<TSyntaxNode> : IArchitectureRule
    where TSyntaxNode : SyntaxNode
{
    private readonly SyntaxScope<TSyntaxNode> _scope;
    public SyntaxArchitectureRule(SyntaxScope<TSyntaxNode> scope)
    {
        _scope = scope;
    }
    
    public IList<IEvaluationBuilder<TSyntaxNode>> Evaluations { get; } = new List<IEvaluationBuilder<TSyntaxNode>>();
    
    public DiagnosticSeverity Severity { get; set; }

    public SyntaxKind[] SyntaxKinds => _scope.SyntaxKinds;
    
    public IEnumerable<DiagnosticDescriptor> Descriptors => Evaluations.Select(e => e.Descriptor);

    public void AddEvaluation(IEvaluationBuilder<TSyntaxNode> evaluation)
    {
        Evaluations.Add(evaluation);
    }
    
    public void Apply(SyntaxNodeAnalysisContext obj)
    {
        var matches = _scope.Identify(obj);
        
        foreach (var match in matches)
        {
            foreach (var evaluation in Evaluations)
            {
                evaluation.Evaluate(obj, match);
            }
        }
    }
}