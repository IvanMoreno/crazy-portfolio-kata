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