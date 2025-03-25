using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public class SymbolScopeBuilder<TSymbol> where TSymbol : ISymbol
{
    private readonly Expression<Func<TSymbol, bool>> _predicate;
    private readonly ISyntaxScope? _syntaxScope;

    public SymbolScopeBuilder(Expression<Func<TSymbol, bool>> predicate, ISyntaxScope? syntaxScope)
    {
        _predicate = predicate;
        _syntaxScope = syntaxScope;
    }
}