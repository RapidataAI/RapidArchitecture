using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

public class SymbolScope<TSymbol> 
    where TSymbol : ISymbol
{
    public SymbolScope(Func<TSymbol, bool> filter, ISyntaxScope? syntaxScope, SymbolKind[] symbolKinds)
    {
        SymbolKinds = symbolKinds;
        Filter = filter;
        SyntaxScope = syntaxScope;
    }

    private Func<TSymbol, bool> Filter { get; }
    private ISyntaxScope? SyntaxScope { get; }
    
    public SymbolKind[] SymbolKinds { get; }

    public IEnumerable<TSymbol> Identify(SymbolAnalysisContext context)
    {
        if(
            context.Symbol is TSymbol specificSymbol && 
            (SyntaxScope is null || context.Symbol.DeclaringSyntaxReferences.Any(sr => SyntaxScope.IsMatch(sr.GetSyntax()))) && 
            Filter(specificSymbol))
        {
            yield return specificSymbol;
        }
    }
}