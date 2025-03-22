using RapidArchitecture.Analyzers.Architecture;

namespace RapidArchitecture.Analyzers.Rules;

public class RapidataCompanyArchitecture : ArchitectureAnalyzer
{
    public RapidataCompanyArchitecture()
    {
        RuleFor(Classes().ResidingInNamespace("Rapidata"))
            .Must(x => x.Identifier.Text.Contains("Rapidata"));
    }
}