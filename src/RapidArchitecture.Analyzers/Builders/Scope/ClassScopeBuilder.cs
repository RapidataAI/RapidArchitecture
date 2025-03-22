using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Builders.Scope;

public class ClassScopeBuilder : IScopeBuilder
{
    public static ClassScopeBuilder Default { get; } = new(_ => true);

    private ClassScopeBuilder(Expression<Func<ClassDeclarationSyntax, bool>> filter)
    {
        _filter = filter;
    }

    private readonly Expression<Func<ClassDeclarationSyntax, bool>> _filter;
    
    public ClassScopeBuilder That(Expression<Func<ClassDeclarationSyntax, bool>> filter)
    {
        return new ClassScopeBuilder(filter);
    }
    
    public ClassScopeBuilder ResidingInNamespace(string namespaceName)
    {
        return That(c => c.Ancestors(true).OfType<BaseNamespaceDeclarationSyntax>().Any(n => n.Name.ToString().StartsWith(namespaceName)));
    }

    public SyntaxKind[] SyntaxKinds => [SyntaxKind.ClassDeclaration];

    public IEnumerable<TypeDeclarationSyntax> Identify(SyntaxNodeAnalysisContext context)
    {
        if(context.Node is ClassDeclarationSyntax classDeclaration && _filter.Compile().Invoke(classDeclaration))
        {
            yield return classDeclaration;
        }
    }
}