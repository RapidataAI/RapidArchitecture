using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            .Custom(x => x.Identifier.Text.Contains("Rapidata"))
            .WithLocation(x => x.Identifier.GetLocation())
            .WithMessage("Type name must contain 'Rapidata'");

        RuleFor(Types().ImplementingInterface("IMessage"))
            .Must()
            .Custom(x => x.Name.EndsWith("Message"))
            .WithMessage("Type name must end with 'Message'");
    }
}