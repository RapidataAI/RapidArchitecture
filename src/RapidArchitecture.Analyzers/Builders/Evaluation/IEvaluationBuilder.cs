using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public interface IEvaluationBuilder<TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    DiagnosticDescriptor Descriptor { get; }
    
    void Evaluate(SyntaxNodeAnalysisContext context, TSyntaxNode match);
    
    Expression<Func<TSyntaxNode, Location>> GetLocation { get; set; }
}