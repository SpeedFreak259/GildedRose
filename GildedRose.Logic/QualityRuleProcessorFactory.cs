// <copyright file="QualityRuleProcessorFactory.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System;
    using System.Collections.Generic;

    using GildedRose.Model;

    /// <summary>
    /// Defines the static factory for getting processors for quality update rules.
    /// </summary>
    public class QualityRuleProcessorFactory
    {
        /// <summary>
        /// The processor rule map.
        /// </summary>
        private static readonly Dictionary<Type, Func<IQualityRuleProcessor>> ProcessorRuleMapping = new Dictionary<Type, Func<IQualityRuleProcessor>>
        {
            { typeof(QualityUpdateRuleQualityAbsolute), () => new QualityRuleProcessorAbsolute() },
            { typeof(QualityUpdateRuleQualityDelta), () => new QualityRuleProcessorDelta() }
        };

        /// <summary>
        /// Gets the processor for the quality update rule for the supplied.
        /// </summary>
        /// <param name="rule">The quality update rule.</param>
        /// <returns>The specific processor for the type of rule supplied.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"> when the rule is not mapped to a processor.</exception>
        public static IQualityRuleProcessor GetProcessorForRule(QualityUpdateRule rule)
        {
            Type ruleType = rule.GetType();

            if (ProcessorRuleMapping.ContainsKey(ruleType))
            {
                return ProcessorRuleMapping[ruleType]();
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}