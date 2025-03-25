using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxRule<TSyntaxNode> where TSyntaxNode : SyntaxNode
{    
    private readonly SyntaxScope<TSyntaxNode> _scope;
    
    public SyntaxRule(SyntaxScope<TSyntaxNode> scope)
    {
        _scope = scope;
    }
    
    public IList<IEvaluationBuilder<TSyntaxNode>> Evaluations { get; } = [];
    
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