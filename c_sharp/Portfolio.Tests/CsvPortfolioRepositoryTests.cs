using NUnit.Framework;

namespace Portfolio.Tests;

public class CsvPortfolioRepositoryTests {
    [Test]
    public void GetAllAssets() {
        var repository = new CsvPortfolioRepository("../../../portfolio.csv");

        var assets = repository.GetAssets();
        
        Assert.That(assets, Is.Not.Null);
        Assert.That(assets.Count(), Is.EqualTo(3));
        Assert.That(assets.First(), Is.EqualTo(Asset.Create((string)"French Wine", new DateTime(day:15, month:1, year:2024), Value.Measurable(100))));
    }
}