using NUnit.Framework;
using static System.Globalization.CultureInfo;

namespace Portfolio.Tests;

public class CsvPortfolioRepositoryTests {
    [Test]
    public void GetAllAssets() {
        var repository = new CsvPortfolioRepository("../../../portfolio.csv");

        var assets = repository.GetAssets();
        
        Assert.That(assets, Is.Not.Null);
        Assert.That(assets.Count(), Is.EqualTo(3));
        Assert.That(assets.First(), Is.EqualTo(new Asset("French Wine", DateTime.Parse("15/1/2024", CurrentCulture), new MeasurableValue(100))));
    }
}