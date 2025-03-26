using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Architecture;
using RapidArchitecture.Analyzers.Builders.Extensions;

namespace RapidArchitecture.Analyzers.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RapidataCompanyArchitecture : ArchitectureAnalyzer
{
    public RapidataCompanyArchitecture()
    {
        RuleFor(Types().ResidingInNamespace("Rapidata"))
            .Should()
            .HaveNameMatching(x => x.Contains("Rapidata"))
            .WithMessage("Type name must contain 'Rapidata'");

        RuleFor(Types().ImplementingInterface("IMessage"))
            .Must()
            .HaveNameMatching(x => x.EndsWith("Message"))
            .WithMessage("Type name must end with 'Message'");
    }
}