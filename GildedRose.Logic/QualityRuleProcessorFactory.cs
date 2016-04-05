// <copyright file="QualityRuleProcessorFactory.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System;
    using System.Collections.Generic;

    using GildedRose.Model;
    using Microsoft.Practices.Unity.Utility;

    using Properties;

    /// <summary>
    /// Defines the static factory for getting processors for quality update rules.
    /// </summary>
    public class QualityRuleProcessorFactory
    {
        /// <summary>
        /// The processor rule map.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No need for encapsulating type.")]
        private Dictionary<Type, Func<IQualityRuleProcessor>> processorRuleMapping = new Dictionary<Type, Func<IQualityRuleProcessor>>
        {
            { typeof(QualityUpdateRuleQualityAbsolute), () => new QualityRuleProcessorAbsolute() },
            { typeof(QualityUpdateRuleQualityDelta), () => new QualityRuleProcessorDelta() }
        };

        /// <summary>
        /// Gets the processor for the quality update rule for the supplied.
        /// </summary>
        /// <param name="rule">The quality update rule.</param>
        /// <returns>The specific processor for the type of rule supplied.</returns>
        /// <exception cref="ArgumentOutOfRangeException"> when the rule is not mapped to a processor.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Parameter is guarded.")]
        public virtual IQualityRuleProcessor GetProcessorForRule(QualityUpdateRule rule)
        {
            Guard.ArgumentNotNull(rule, nameof(rule));

            Type ruleType = rule.GetType();

            if (this.processorRuleMapping.ContainsKey(ruleType))
            {
                return this.processorRuleMapping[ruleType]();
            }

            throw new ArgumentOutOfRangeException(string.Format(ExceptionMessages.NoProcessorForRule, rule.GetType().Name));
        }
    }
}