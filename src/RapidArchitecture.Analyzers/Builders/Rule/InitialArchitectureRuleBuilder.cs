using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class InitialArchitectureRuleBuilder<TSyntaxNode> 
    where TSyntaxNode : SyntaxNode
{
    private readonly ArchitectureRule<TSyntaxNode> _architectureRule;
    public InitialArchitectureRuleBuilder(ArchitectureRule<TSyntaxNode>  architectureRule)
    {
        _architectureRule = architectureRule;
    }

    public EvaluationArchitectureRuleBuilder<TSyntaxNode> Must()
    {
        _architectureRule.Severity = DiagnosticSeverity.Error;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> Should()
    {
        _architectureRule.Severity = DiagnosticSeverity.Warning;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> May()
    {
        _architectureRule.Severity = DiagnosticSeverity.Info;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> MustNot()
    {
        _architectureRule.Severity = DiagnosticSeverity.Hidden;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_architectureRule);
    }
}