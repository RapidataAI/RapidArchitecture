using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class InitialArchitectureRuleBuilder<TSyntaxNode> 
    where TSyntaxNode : SyntaxNode
{
    private readonly SyntaxArchitectureRule<TSyntaxNode> _syntaxArchitectureRule;
    public InitialArchitectureRuleBuilder(SyntaxArchitectureRule<TSyntaxNode>  syntaxArchitectureRule)
    {
        _syntaxArchitectureRule = syntaxArchitectureRule;
    }

    public EvaluationArchitectureRuleBuilder<TSyntaxNode> Must()
    {
        _syntaxArchitectureRule.Severity = DiagnosticSeverity.Error;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_syntaxArchitectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> Should()
    {
        _syntaxArchitectureRule.Severity = DiagnosticSeverity.Warning;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_syntaxArchitectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> May()
    {
        _syntaxArchitectureRule.Severity = DiagnosticSeverity.Info;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_syntaxArchitectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TSyntaxNode> MustNot()
    {
        _syntaxArchitectureRule.Severity = DiagnosticSeverity.Hidden;
        return new EvaluationArchitectureRuleBuilder<TSyntaxNode>(_syntaxArchitectureRule);
    }
}