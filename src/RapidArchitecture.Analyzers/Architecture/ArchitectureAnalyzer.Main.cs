using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Architecture;

public abstract partial class ArchitectureAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.CreateRange(_rules.SelectMany(x => x.Descriptors));

    public override void Initialize(AnalysisContext context)
    {
        // You must call this method to avoid analyzing generated code.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // You must call this method to enable the Concurrent Execution.
        context.EnableConcurrentExecution();

        // Subscribe to the Syntax Node with the appropriate 'SyntaxKind' (ClassDeclaration) action.
        // To figure out which Syntax Nodes you should choose, consider installing the Roslyn syntax tree viewer plugin Rossynt: https://plugins.jetbrains.com/plugin/16902-rossynt/

        foreach (var rule in _rules)
        {
            rule.Apply(context);
        }   

        // Check other 'context.Register...' methods that might be helpful for your purposes.
    }
}