using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

public interface IArchitectureRule
{
    void Apply(SyntaxNodeAnalysisContext obj);
    
    SyntaxKind[] SyntaxKinds { get; }
    
    IEnumerable<DiagnosticDescriptor> Descriptors { get; }
}