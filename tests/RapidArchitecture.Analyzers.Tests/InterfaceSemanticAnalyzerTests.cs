using System.Threading.Tasks;
using RapidArchitecture.Analyzers.Architecture;
using RapidArchitecture.Analyzers.Builders.Extensions;
using Xunit;
using AnalyzerTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<
    RapidArchitecture.Analyzers.Tests.InterfaceSemanticAnalyzerTests.RapidataCompanyArchitecture,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace RapidArchitecture.Analyzers.Tests;

public class InterfaceSemanticAnalyzerTests
{
    [Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer(Microsoft.CodeAnalysis.LanguageNames.CSharp)]
    public class RapidataCompanyArchitecture : ArchitectureAnalyzer
    {
        public RapidataCompanyArchitecture()
        {
            RuleFor(Types().ImplementingInterface("global::SomeNamespace.IMessage"))
                .May()
                .Custom(x => x.Name.EndsWith("Message"))
                .WithMessage("Type name must end with 'Message'");
        }
    }    
    
    [Fact]
    public async Task ClassImplementingIMessage_NotEndingWithMessage_ShouldAlert()
    {
        const string text = @"
namespace SomeNamespace;

public interface IMessage;

public class [|Something|] : IMessage
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