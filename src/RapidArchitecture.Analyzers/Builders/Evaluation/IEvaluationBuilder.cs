using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public interface IEvaluationBuilder
{
    DiagnosticDescriptor Descriptor { get; }
    
    void Evaluate(SyntaxNodeAnalysisContext context, TypeDeclarationSyntax match);
}