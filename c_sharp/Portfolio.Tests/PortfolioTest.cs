using NUnit.Framework;

namespace Portfolio.Tests;

public class MockDisplay : Display {
    public MeasurableValue PortfolioValue { get; private set; }
    public Asset UnicornAsset { get; private set; }

    public void ShowPortfolio(MeasurableValue portfolioValue) {
        this.PortfolioValue = portfolioValue;
    }
    
    public void ShowUnicorn(Asset asset) {
        UnicornAsset = asset;
    }
}

public class StubRepository : PortfolioRepository {
    public IEnumerable<Asset> Assets { get; set; }
    
    public IEnumerable<Asset> GetAssets() {
        return Assets;
    }
}

public class PortfolioTest
{
    [Test]
    public void ShowUnicornAsset_IfExists() {
        var display = new MockDisplay();
        var repository = new StubRepository {
            Assets = new[] {
                new Asset("Unicorn", new DateTime(), new MeasurableValue(100)),
                new Asset("French Wine", new DateTime(), new MeasurableValue(100))
            }
        };
        var app = new Portfolio(display, repository);
        
        app.ComputePortfolioValue();

        Assert.That(display.UnicornAsset, Is.Not.Null);
        Assert.That(display.PortfolioValue, Is.Null);
    }

    [Test]
    public void ShowPortfolioValue() {
        var display = new MockDisplay();
        var app = new Portfolio(display, new CsvPortfolioRepository("../../../portfolio_no_unicorn.csv"));
        
        app.ComputePortfolioValue();
        
        Assert.That(display.PortfolioValue.Get(), Is.EqualTo(120));
    }
}