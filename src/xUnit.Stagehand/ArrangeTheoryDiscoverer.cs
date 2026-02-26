using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xUnit.Stagehand
{
    public class ArrangeTheoryDiscoverer : TheoryDiscoverer
    {
        public ArrangeTheoryDiscoverer(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink) { }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForTheory(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo theoryAttribute)
        {
            // We yield the Theory case to allow the method to return a 'Stage' 
            // while still being treated as a parameterized test.
            yield return new XunitTheoryTestCase(
                DiagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(),
                TestMethodDisplayOptions.None,
                testMethod);
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForDataRow(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo theoryAttribute,
            object[] dataRow)
        {
            // This handles individual data rows (like InlineData)
            yield return new XunitTestCase(
                DiagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(),
                TestMethodDisplayOptions.None,
                testMethod,
                dataRow);
        }
    }
}