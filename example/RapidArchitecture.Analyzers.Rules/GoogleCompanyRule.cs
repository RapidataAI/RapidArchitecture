using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RapidArchitecture.Analyzers.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class GoogleCompanyRule : SampleSyntaxAnalyzer
{
    protected override string CompanyName => "Google";
}