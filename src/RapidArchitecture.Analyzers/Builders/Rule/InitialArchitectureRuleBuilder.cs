using Microsoft.CodeAnalysis;
using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class InitialArchitectureRuleBuilder<TArchitectureRule, TAnalyze> 
    where TArchitectureRule : class, IArchitectureRule<TAnalyze> 
    where TAnalyze : class
{
    private readonly TArchitectureRule _architectureRule;
    public InitialArchitectureRuleBuilder(TArchitectureRule  architectureRule)
    {
        _architectureRule = architectureRule;
    }

    public EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze> Must()
    {
        _architectureRule.Severity = DiagnosticSeverity.Error;
        return new EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze> Should()
    {
        _architectureRule.Severity = DiagnosticSeverity.Warning;
        return new EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze> May()
    {
        _architectureRule.Severity = DiagnosticSeverity.Info;
        return new EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze>(_architectureRule);
    }
    
    public EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze> MustNot()
    {
        _architectureRule.Severity = DiagnosticSeverity.Hidden;
        return new EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze>(_architectureRule);
    }
}