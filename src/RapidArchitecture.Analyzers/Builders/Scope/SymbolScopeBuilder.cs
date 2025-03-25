using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public class SymbolScopeBuilder<TSymbol> 
    where TSymbol : ISymbol
{
    private readonly Expression<Func<TSymbol, bool>> _filter;
    private readonly ISyntaxScope? _syntaxScope;
    private readonly SymbolKind[] _symbolKinds;

    public SymbolScopeBuilder(Expression<Func<TSymbol, bool>> filter, ISyntaxScope? syntaxScope, SymbolKind[] symbolKinds)
    {
        _filter = filter;
        _syntaxScope = syntaxScope;
        _symbolKinds = symbolKinds;
    }

    public SymbolScope<TSymbol> Build()
    {
        return new SymbolScope<TSymbol>(_filter, _syntaxScope, _symbolKinds);
    }
}