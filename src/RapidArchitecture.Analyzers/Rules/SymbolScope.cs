using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

public class SymbolScope<TSymbol> where TSymbol : ISymbol
{
    public SymbolScope(Expression<Func<TSymbol, bool>> filter, ISyntaxScope? syntaxScope)
    {
        Filter = filter.Compile();
        SyntaxScope = syntaxScope;
    }
    

    private Func<TSymbol, bool> Filter { get; }
    private ISyntaxScope? SyntaxScope { get; }

    
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