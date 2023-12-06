using CynkDemo.CynkLib;

namespace CynkDemo.Tests;

[TestClass]
public class CynkTests
{
    private List<Product> _sourceProducts = null!;
    private List<Product> _targetProducts = null!;

    [TestInitialize]
    public void Setup()
    {
        _sourceProducts = new()
        {
            new() { Id = 1, Title = "Product 1" },
            new() { Id = 2, Title = "Product 2" }
        };
        _targetProducts = new List<Product>
        {
            new() { Id = 1, Title = "Product 1" },
            new() { Id = 2, Title = "Product 3" }
        };
    }

    [TestMethod]
    public void CynkDeleteTest()
    {
        // Arrange
        var source = new List<string> { "Apple", "Banana", "Cranapple", "Date" };
        var target = new List<string> { "Apple", "Cranapple", "Date" };

        // Act
        var results = new Cynk<string>(source)
            .Sync(target);

        // Assert
        Assert.IsTrue(results.Deleted.Count == 1);
    }

    [TestMethod]
    public void CynkUpdateTest()
    {
        // Arrange
        var source = new List<string> { "Apple", "Banana", "Cranapple", "Date" };
        var target = new List<string> { "Apple", "Banana", "Cranapple", "Data" };

        // Act
        var results = new Cynk<string>(source)
            .Sync(target);

        // Assert
        Assert.IsTrue(results.Added.Count == 1);
        Assert.IsTrue(results.Deleted.Count == 1);
        Assert.IsTrue(results.Updated.Count == 0);
    }

    [TestMethod]
    public void CynkAddedTest()
    {
        // Arrange
        var source = new List<string> { "Apple", "Banana", "Cranapple", "Date" };
        var target = new List<string> { "Apple", "Banana", "Cranapple", "Date", "Cherry" };

        // Act
        var results = new Cynk<string>(source)
            .Sync(target);

        // Assert
        Assert.IsTrue(results.Added.Count == 1);
    }

    [TestMethod]
    public void CynkUpdateObjectTest()
    {
        // Arrange (in Setup)

        // Act
        var results = new CynkWithInt<Product>(_sourceProducts, item => item.Id)
            .Sync(_targetProducts);

        // Assert
        Assert.IsTrue(results.Updated.Count == 1);
        Assert.IsTrue(results.Updated[0].Title.Equals("Product 3"));
    }
}
