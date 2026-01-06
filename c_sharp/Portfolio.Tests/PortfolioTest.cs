using NUnit.Framework;

namespace Portfolio.Tests;

public class SpyDisplay : PortfolioDisplay {
    public AssetValue LastPortfolioValue { get; private set; }
    public bool DisplayedUnicorn { get; private set; }
    
    public void ShowPortfolioValue(AssetValue portfolioValue) {
        LastPortfolioValue = portfolioValue;
    }

    public void ShowUnicorn(Asset asset) {
        DisplayedUnicorn = true;
    }
}

public class PortfolioTest
{
    [Test]
    public void ComputeValue()
    {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio(spyDisplay, new CsvAssetsLoader("../../../portfolio_no_unicorn.csv"));

        app.ComputePortfolioValue();

        Assert.That(120, Is.EqualTo(spyDisplay.LastPortfolioValue.Get()));
    }

    [Test]
    public void ComputeValue_FailsWhenUnicornAppears() {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio(spyDisplay, new CsvAssetsLoader("../../../portfolio.csv"));

        app.ComputePortfolioValue();

        Assert.That(spyDisplay.DisplayedUnicorn, Is.True);
    }
}