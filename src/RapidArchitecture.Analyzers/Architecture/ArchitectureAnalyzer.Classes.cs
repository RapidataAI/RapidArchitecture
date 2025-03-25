using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Architecture;

public partial class ArchitectureAnalyzer
{
    protected static SyntaxScopeBuilder<ClassDeclarationSyntax> Classes() => new(x => true, [SyntaxKind.ClassDeclaration]);
    protected static SyntaxScopeBuilder<RecordDeclarationSyntax> Records() => new(x => true, [SyntaxKind.RecordDeclaration]);

    protected static SyntaxScopeBuilder<TypeDeclarationSyntax> Types() => new(x => true,
    [
        SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration, SyntaxKind.InterfaceDeclaration,
        SyntaxKind.StructDeclaration
    ]);
}