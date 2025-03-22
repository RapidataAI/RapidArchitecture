using System.Collections.Generic;
using RapidArchitecture.Analyzers.Builders.Rule;
using RapidArchitecture.Analyzers.Builders.Scope;

namespace RapidArchitecture.Analyzers.Architecture;

public partial class ArchitectureAnalyzer
{
    private readonly IList<ArchitectureRuleBuilder> _ruleBuilder = [];

    public ArchitectureRuleBuilder RuleFor(IScopeBuilder scopeBuilder)
    {
        var rule = new ArchitectureRuleBuilder(scopeBuilder);
        _ruleBuilder.Add(rule);
        return rule;
    }
}