using System.Threading.Tasks;
using Xunit;
using AnalyzerTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerTest<
    RapidArchitecture.Analyzers.Rules.GoogleCompanyRule,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;


namespace RapidArchitecture.Analyzers.Tests;

public class SampleSyntaxAnalyzerTests
{
    [Fact]
    public async Task ClassWithMyCompanyTitle_AlertDiagnostic()
    {
        const string text = @"
public class [|Google|]
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