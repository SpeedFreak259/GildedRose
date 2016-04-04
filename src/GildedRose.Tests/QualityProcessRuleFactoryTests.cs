namespace GildedRose.Tests
{
    using System;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Xunit;

    /// <summary>
    /// Defines the tests for the <see cref="QualityRuleProcessorFactory"/>
    /// </summary>
    public class QualityProcessRuleFactoryTests
    {
        /// <summary>
        /// Gets the blank delta quality udpate rule.
        /// </summary>
        /// <value>
        /// The blank delta rule.
        /// </value>
        private static QualityUpdateRuleQualityDelta BlankDeltaRule => new QualityUpdateRuleQualityDelta();

        /// <summary>
        /// Gets the blank absolute quality udpate rule.
        /// </summary>
        /// <value>
        /// The blank absolute rule.
        /// </value>
        private static QualityUpdateRuleQualityAbsolute BlankAbsoluteRule => new QualityUpdateRuleQualityAbsolute();

        /// <summary>
        /// Asserts that the <see cref="QualityRuleProcessorFactory"/> returns the appropriate processor for <see cref="QualityUpdateRuleQualityDelta"/>.
        /// </summary>
        [Fact]
        public void GivenDeltaRule_WhenFactoryCalled_ThenDeltaProcessorInstanceReturned()
        {
            this.FactoryAssertion(BlankDeltaRule, typeof(QualityRuleProcessorDelta));
        }

        /// <summary>
        /// Asserts that the <see cref="QualityRuleProcessorFactory"/> returns the appropriate processor for <see cref="QualityRuleProcessorAbsolute"/>.
        /// </summary>
        [Fact]
        public void GivenAbsoluteRule_WhenFactoryCalled_ThenAbsoluteProcessorInstanceReturned()
        {
            this.FactoryAssertion(BlankAbsoluteRule, typeof(QualityRuleProcessorAbsolute));
        }

        /// <summary>
        /// Asserts that <see cref="QualityRuleProcessorFactory"/> returns the specified type to process the supplied rule.
        /// </summary>
        /// <typeparam name="TRule">The type of the rule, must derive from <see cref="QualityUpdateRule"/>.</typeparam>
        /// <param name="rule">The quality update rule.</param>
        /// <param name="expectedType">The expected processor type.</param>
        private void FactoryAssertion<TRule>(TRule rule, Type expectedType)
            where TRule : QualityUpdateRule
        {
            // Act
            var processor = QualityRuleProcessorFactory.GetProcessorForRule(rule);

            // Assert
            Assert.IsAssignableFrom(expectedType, processor);
        }
    }
}
