using Xunit;
using Xunit.Sdk;

namespace xUnit.Stagehand
{
    /// <summary>
    /// Identifies a method as an xUnit Fact that is allowed to return a value.
    /// This allows you to write standalone tests that also act as reusable Arrange/Creator steps for other tests.
    /// </summary>
    [XunitTestCaseDiscoverer("xUnit.Stagehand.ArrangeFactDiscoverer", "xUnit.Stagehand")]
    public class ArrangeFactAttribute : FactAttribute
    {
    }
}

