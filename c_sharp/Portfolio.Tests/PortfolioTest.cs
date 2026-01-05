using NUnit.Framework;

namespace Portfolio.Tests;

public class PortfolioTest
{
    [Test]
    public void Fix_Me()
    {
        var app = new Portfolio("../../../portfolio.csv", new LogDisplay());

        app.ComputePortfolioValue();

        Assert.That("fixme", Is.EqualTo("fixme"));
    }
}