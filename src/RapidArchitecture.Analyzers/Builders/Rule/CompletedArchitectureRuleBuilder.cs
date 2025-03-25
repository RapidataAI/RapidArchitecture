using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class CompletedArchitectureRuleBuilder<TArchitectureRule, TAnalyze> : EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze>
    where TArchitectureRule : class, IArchitectureRule<TAnalyze> 
    where TAnalyze : class
{
    public CompletedArchitectureRuleBuilder(TArchitectureRule architectureRule) : base(architectureRule)
    {
    }
}