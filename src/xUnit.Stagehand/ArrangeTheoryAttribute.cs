using Xunit;
using Xunit.Sdk;

namespace xUnit.Stagehand
{
    /// <summary>
    /// Identifies a method as an xUnit Theory that is allowed to return a value.
    /// Used for parameterized "Arrange" stages.
    /// </summary>
    [XunitTestCaseDiscoverer("xUnit.Stagehand.ArrangeTheoryDiscoverer", "xUnit.Stagehand")]
    public class ArrangeTheoryAttribute : TheoryAttribute { }
}