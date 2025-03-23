using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public class ExpressionEvaluationBuilder<TSyntaxNode> : IEvaluationBuilder<TSyntaxNode> where TSyntaxNode : SyntaxNode
{
    public ExpressionEvaluationBuilder(Expression<Func<TSyntaxNode, bool>> evaluation, Expression<Func<TSyntaxNode, Location>>? location, DiagnosticSeverity severity)
    {
        Evaluation = evaluation.Compile();
        GetLocation = location ?? (static x => x.GetLocation());
        Descriptor = new DiagnosticDescriptor("RA0001", "Title", "Message", "Category", severity, true);
    }

    private Func<TSyntaxNode,bool> Evaluation { get; }
    
    public DiagnosticDescriptor Descriptor { get; set; } 
    
    public Expression<Func<TSyntaxNode, Location>> GetLocation { get; set; }
    
    public void Evaluate(SyntaxNodeAnalysisContext context, TSyntaxNode match)
    {
        if (!Evaluation(match))
        {
            context.ReportDiagnostic(Diagnostic.Create(Descriptor, GetLocation.Compile()(match)));
        }
    }

}