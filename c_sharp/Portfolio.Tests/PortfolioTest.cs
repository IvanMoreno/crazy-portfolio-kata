using NUnit.Framework;

namespace Portfolio.Tests;

public class SpyDisplay : PortfolioDisplay {
    public AssetValue LastPortfolioValue { get; private set; }
    public bool DisplayedUnicorn { get; private set; }
    
    public void Display(AssetValue portfolioValue) {
        LastPortfolioValue = portfolioValue;
    }

    public void DisplayUnicorn(Asset asset) {
        DisplayedUnicorn = true;
    }
}

public class PortfolioTest
{
    [Test]
    public void ComputeValue()
    {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio("../../../portfolio_no_unicorn.csv", spyDisplay, new CsvPortfolioLoader("../../../portfolio_no_unicorn.csv"));

        app.ComputePortfolioValue();

        Assert.That(120, Is.EqualTo(spyDisplay.LastPortfolioValue.Get()));
    }

    [Test]
    public void ComputeValue_FailsWhenUnicornAppears() {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio("../../../portfolio.csv", spyDisplay, new CsvPortfolioLoader("../../../portfolio.csv"));

        app.ComputePortfolioValue();

        Assert.That(spyDisplay.DisplayedUnicorn, Is.True);
    }
}