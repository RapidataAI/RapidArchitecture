using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using RapidArchitecture.Analyzers.Architecture;
using RapidArchitecture.Analyzers.Builders.Extensions;
using Xunit;
using AnalyzerTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<
    RapidArchitecture.Analyzers.Tests.NamespaceSyntaxAnalyzerTests.RapidataCompanyArchitecture,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace RapidArchitecture.Analyzers.Tests;

public class NamespaceSyntaxAnalyzerTests
{
    [Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer(Microsoft.CodeAnalysis.LanguageNames.CSharp)]
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
    
    [Fact]
    public async Task ClassWithoutRapidataName_AlertDiagnostic()
    {
        const string text = @"
namespace Rapidata;

public class [|SomeClass|]
{
}
";

        var context = new AnalyzerTest
        {
            TestCode = text,
            
        };

        await context.RunAsync();
    }
    
    [Fact]
    public async Task ClassOutsideRapidataNamespace_NoAlertDiagnostic()
    {
        const string text = @"
namespace SomeOtherNamespace;

public class SomeClass
{
}
";

        var context = new AnalyzerTest
        {
            TestCode = text
        };

        await context.RunAsync();
    }
    
    [Fact]
    public async Task ClassWithRapidataName_NoAlertDiagnostic()
    {
        const string text = @"
namespace Rapidata;

public class SomeRapidataClass
{
}
";

        var context = new AnalyzerTest
        {
            TestCode = text
        };

        await context.RunAsync();
    }
}