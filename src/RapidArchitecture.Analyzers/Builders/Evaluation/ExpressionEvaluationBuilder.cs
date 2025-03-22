using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Evaluation;

public class ExpressionEvaluationBuilder : IEvaluationBuilder
{
    public ExpressionEvaluationBuilder(Func<TypeDeclarationSyntax, bool> expression)
    {
        Expression = expression;
        Descriptor = new DiagnosticDescriptor("RA0001", "Title", "Message", "Category", DiagnosticSeverity.Warning, true);
    }

    private Func<TypeDeclarationSyntax,bool> Expression { get; }
    
    public DiagnosticDescriptor Descriptor { get; }

    public void Evaluate(SyntaxNodeAnalysisContext context, TypeDeclarationSyntax match)
    {
        if (!Expression(match))
        {
            context.ReportDiagnostic(Diagnostic.Create(Descriptor, match.Identifier.GetLocation()));
        }
    }
}