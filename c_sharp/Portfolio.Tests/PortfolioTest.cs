using NUnit.Framework;

namespace Portfolio.Tests;

public class SpyDisplay : PortfolioDisplay {
    public AssetValue LastPortfolioValue { get; private set; }
    
    public void Display(AssetValue portfolioValue) {
        LastPortfolioValue = portfolioValue;
    }
}

public class PortfolioTest
{
    [Test]
    public void ComputeValue()
    {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio("../../../portfolio_no_unicorn.csv", spyDisplay);

        app.ComputePortfolioValue();

        Assert.That(120, Is.EqualTo(spyDisplay.LastPortfolioValue.Get()));
    }

    [Test]
    public void ComputeValue_FailsWhenUnicornAppears() {
        var spyDisplay = new SpyDisplay();
        var app = new Portfolio("../../../portfolio.csv", spyDisplay);

        app.ComputePortfolioValue();

        Assert.That(spyDisplay.LastPortfolioValue, Is.Null);
    }
}