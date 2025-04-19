using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Builders.Evaluation;
using RapidArchitecture.Analyzers.Builders.Locating;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Rules;

public class SyntaxArchitectureRule<TSyntaxNode> : IArchitectureRule<TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    private readonly List<IEvaluator<TSyntaxNode>> _evaluationBuilders = [];
    public SyntaxArchitectureRule(SyntaxScope<TSyntaxNode> scope)
    {
        Scope = scope;
    }

    public IReadOnlyList<IEvaluator<TSyntaxNode>> Evaluations => _evaluationBuilders.AsReadOnly();
    
    public void AddEvaluation(Func<TSyntaxNode, bool> evaluation)
    {
        _evaluationBuilders.Add(new FunctionEvaluator<TSyntaxNode>(evaluation, Severity, new FunctionLocator<TSyntaxNode>(e => e.GetLocation())));
    }

    public DiagnosticSeverity Severity { get; set; }

    private SyntaxScope<TSyntaxNode> Scope { get; }
    
    public IEnumerable<DiagnosticDescriptor> Descriptors => Evaluations.Select(e => e.Descriptor);
    
    private void Apply(SyntaxNodeAnalysisContext context)
    {
        var matches = Scope.Identify(context);
        
        foreach (var match in matches)
        {
            foreach (var evaluation in _evaluationBuilders)
            {
                foreach (var diagnostic in evaluation.Evaluate(match))
                {
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    public void Apply(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(Apply, Scope.SyntaxKinds);
    }
}