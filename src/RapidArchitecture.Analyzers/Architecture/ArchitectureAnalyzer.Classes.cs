using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Architecture;

public partial class ArchitectureAnalyzer
{
    protected ClassScopeBuilder Classes() => ClassScopeBuilder.Default;
}