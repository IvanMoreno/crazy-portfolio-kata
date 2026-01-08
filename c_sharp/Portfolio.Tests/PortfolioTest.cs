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

public class PortfolioTest
{
    [Test]
    public void ShowUnicornAsset_IfExists() {
        var display = new MockDisplay();
        var app = new Portfolio("../../../portfolio.csv", display);

        app.ComputePortfolioValue();

        Assert.That(display.UnicornAsset, Is.Not.Null);
    }
}