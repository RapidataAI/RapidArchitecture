# RapidArchitecture - Fluent Architecture Rules for Roslyn

This package enables developers to create fluent rules for their architecture, leveraging Roslyn to generate errors, warnings, and other diagnostics. It allows you to define custom architecture rules in a clean and fluent syntax, providing feedback directly within your development environment.

## Installation

Make sure to create a new project that will store your Analyzer rules. This project can look like this:

```xml
<Project Sdk="Microsoft.NET.Sdk">
    
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
        <IsPackable>false</IsPackable>
        <Nullable>enable</Nullable>
        <LangVersion>latest</LangVersion>

        <EnforceExtendedAnalyzerRules>true</EnforceExtendedAnalyzerRules>
        <IsRoslynComponent>true</IsRoslynComponent>

        <RootNamespace>**REPLACE WITH YOUR ASSEMBLY NAME**</RootNamespace>
        <AssemblyName>**REPLACE WITH YOUR ASSEMBLY NAME**</AssemblyName>
    </PropertyGroup>
    
</Project>

```

You can install this package via NuGet:

```
dotnet add package RapidArchitecture
```

Next you can reference your newly created Analyzer project from the projects that you want to analyze. Add the following configuration to your project reference:

```xml
    <ItemGroup>
        <ProjectReference Include="**PATH TO PROJECT**" OutputItemType="Analyzer" ReferenceOutputAssembly="false"/>
    </ItemGroup>
```

## Usage

Once installed, you can create your own architecture rules using a fluent API. You can create new rules from within an analyzer class. Here is an example of a simple rule that checks if a type name contains a specific string:

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RapidArchitecture.Analyzers.Architecture;
using RapidArchitecture.Analyzers.Builders.Extensions;

namespace Rapidata.Analyzers.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ArchitectureRules : ArchitectureAnalyzer
{
    public ArchitectureRules()
    {
        RuleFor(Types().ImplementingInterface("IMessage"))
            .Must()
            .HaveNameMatching(x => x.EndsWith("Message"))
            .WithMessage("Type name must end with 'Message'");
        
        RuleFor(Types().ImplementingInterface("IEvent"))
            .Must()
            .HaveNameMatching(x => x.EndsWith("Event"))
            .WithMessage("Type name must end with 'Event'");
    }
}
```

## Contributing

If you'd like to contribute, please feel free to submit a pull request. We welcome any improvements or suggestions.

## License

This package is licensed under the [MIT License](LICENSE).