namespace Portfolio.Tests;

public class MockDisplay : Display {
    public Value PortfolioValue { get; private set; }
    public Asset UnicornAsset { get; private set; }

    public void ShowPortfolio(Value portfolioValue) {
        this.PortfolioValue = portfolioValue;
    }
    
    public void ShowUnicorn(Asset asset) {
        UnicornAsset = asset;
    }
}