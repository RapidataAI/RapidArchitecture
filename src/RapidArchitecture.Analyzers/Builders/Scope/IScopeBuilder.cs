using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public interface IScopeBuilder
{
    SyntaxKind[] SyntaxKinds { get; }
    
    IEnumerable<TypeDeclarationSyntax> Identify(SyntaxNodeAnalysisContext context);
}