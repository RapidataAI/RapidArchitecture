using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxArchitectureRule<TSyntaxNode> : IArchitectureRule<TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    private readonly List<IEvaluationBuilder<TSyntaxNode>> _evaluationBuilders = [];
    public SyntaxArchitectureRule(SyntaxScope<TSyntaxNode> scope)
    {
        Scope = scope;
    }

    public IList<IEvaluationBuilder<TSyntaxNode>> Evaluations => _evaluationBuilders;
    
    public DiagnosticSeverity Severity { get; set; }

    private SyntaxScope<TSyntaxNode> Scope { get; }
    
    public IEnumerable<DiagnosticDescriptor> Descriptors => Evaluations.Select(e => e.Descriptor);

    public void AddEvaluation(IEvaluationBuilder<TSyntaxNode> evaluation)
    {
        _evaluationBuilders.Add(evaluation);
    }
    
    private void Apply(SyntaxNodeAnalysisContext obj)
    {
        var matches = Scope.Identify(obj);
        
        foreach (var match in matches)
        {
            foreach (var evaluation in _evaluationBuilders)
            {
                evaluation.Evaluate(obj, match);
            }
        }
    }

    public void Apply(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(Apply, Scope.SyntaxKinds);
    }
}