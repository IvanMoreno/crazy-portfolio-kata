using NUnit.Framework;

namespace Portfolio.Tests;

public class MockDisplay : Display {
    public void ShowPortfolio(MeasurableValue portfolioValue) {
        throw new NotImplementedException();
    }
    public void ShowUnicorn(Asset asset) {
        throw new NotImplementedException();
    }
}

public class PortfolioTest
{
    [Test]
    public void Fix_Me() {
        var display = new MockDisplay();
        var app = new Portfolio("../../../portfolio.csv", display);

        app.ComputePortfolioValue();

        Assert.That("fixme", Is.EqualTo("fixme"));
    }
}