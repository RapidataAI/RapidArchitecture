using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Architecture;

namespace RapidArchitecture.Analyzers.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RapidataCompanyArchitecture : ArchitectureAnalyzer
{
    public RapidataCompanyArchitecture()
    {
        RuleFor(Classes().ResidingInNamespace("Rapidata"))
            .Must()
            .Custom(x => x.Identifier.Text.Contains("Rapidata"))
            .WithLocation(x => x.Identifier.GetLocation());
    }
}