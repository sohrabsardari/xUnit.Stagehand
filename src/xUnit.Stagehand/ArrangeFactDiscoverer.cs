using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xUnit.Stagehand
{
    /// <summary>
    /// The custom discoverer for <see cref="ArrangeFactAttribute"/>.
    /// Bypasses xUnit's default return type validation (which strictly requires void or Task),
    /// allowing test methods to return domain objects safely.
    /// </summary>
    public class ArrangeFactDiscoverer : IXunitTestCaseDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public ArrangeFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo factAttribute)
        {
            // Yielding XunitTestCase directly instructs the xUnit runner to execute the method 
            // and simply discard the return value, preventing the standard "Test methods must return void" error.
            yield return new XunitTestCase(
                _diagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(),
                TestMethodDisplayOptions.None,
                testMethod);
        }
    }
}