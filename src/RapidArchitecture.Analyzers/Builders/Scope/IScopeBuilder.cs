using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public interface IScopeBuilder<out TSyntaxNode>
    where TSyntaxNode : SyntaxNode
{
    SyntaxKind[] SyntaxKinds { get; }
    
    IEnumerable<TSyntaxNode> Identify(SyntaxNodeAnalysisContext context);
}