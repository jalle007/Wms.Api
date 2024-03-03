namespace Wms.Tests.Base;
using Xunit;

// Grouping tests into a single test collection
// to avoid the overhead of creating new instances of IntegrationTestFactory for each test.
// Also, this ensures that all tests in this collection run sequentially.
[CollectionDefinition("WmsTestsCollection")]
public class WmsTestsCollectionDefinition : ICollectionFixture<WmsTestFactory>
{
}
