using RapidArchitecture.Analyzers.Rules;

namespace RapidArchitecture.Analyzers.Builders.Rule;

public class EvaluationArchitectureRuleBuilder<TArchitectureRule, TAnalyze> 
    where TArchitectureRule : class, IArchitectureRule<TAnalyze> 
    where TAnalyze : class
{
    public readonly TArchitectureRule ArchitectureRule;
    public EvaluationArchitectureRuleBuilder(TArchitectureRule architectureRule)
    {
        ArchitectureRule = architectureRule;
    }
}