using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public class ExpressionEvaluator<TSyntaxNode> : IEvaluator<TSyntaxNode> where TSyntaxNode : SyntaxNode
{
    public ExpressionEvaluator(Expression<Func<TSyntaxNode, bool>> evaluation, Expression<Func<TSyntaxNode, Location>>? location, DiagnosticSeverity severity)
    {
        Evaluation = evaluation.Compile();
        GetLocation = location ?? (static x => x.GetLocation());
        Descriptor = new DiagnosticDescriptor("RA0001", "Title", "Message", "Category", severity, true);
    }

    private Func<TSyntaxNode,bool> Evaluation { get; }
    
    public DiagnosticDescriptor Descriptor { get; set; }

    public IEnumerable<Diagnostic> Evaluate(TSyntaxNode match)
    {
        if (!Evaluation(match))
        {
            yield return Diagnostic.Create(Descriptor, GetLocation.Compile()(match));
        }
    }

    public Expression<Func<TSyntaxNode, Location>> GetLocation { get; set; }
}