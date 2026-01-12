using NUnit.Framework;

namespace Portfolio.Tests;

public class PortfolioTest {
    [Test]
    public void ShowUnicornAsset_IfExists() {
        var display = new MockDisplay();
        var repository = new StubRepository {
            Assets = new[] {
                Asset.Create((string)"Unicorn", new DateTime(), Value.Measurable(100)),
                Asset.Create((string)"French Wine", new DateTime(), Value.Measurable(100))
            }
        };
        var app = new Portfolio(display, repository, new RealtimeClock());

        app.ComputePortfolioValue();

        Assert.That(display.UnicornAsset, Is.Not.Null);
        Assert.That(display.PortfolioValue, Is.Null);
    }

    [Test]
    public void DoNotShowUnicorn_IfValueIsZero() {
        var display = new MockDisplay();
        var repository = new StubRepository {
            Assets = new[] {
                Asset.Create((string)"Unicorn", new DateTime(), Value.Measurable(0))
            }
        };
        var app = new Portfolio(display, repository, new RealtimeClock());
        
        app.ComputePortfolioValue();
        
        Assert.That(display.UnicornAsset, Is.Null);
    }

    [Test]
    public void ShowPortfolioValue() {
        var display = new MockDisplay();
        var app = new Portfolio(display, new CsvPortfolioRepository("../../../portfolio_no_unicorn.csv"), new RealtimeClock());

        app.ComputePortfolioValue();

        Assert.That(display.PortfolioValue.Get(), Is.EqualTo(120));
    }
}