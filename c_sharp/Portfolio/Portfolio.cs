namespace Portfolio;

public class Portfolio
{
    readonly Display _display;
    readonly PortfolioRepository _portfolioRepository;

    public Portfolio(Display display, PortfolioRepository portfolioRepository) {
        _portfolioRepository = portfolioRepository;
        _display = display;
    }

    public void ComputePortfolioValue()
    {
        var now = DateTime.Now;
        var portfolioValue = new MeasurableValue(0);
        
        foreach (var asset in _portfolioRepository.GetAssets()) {
            if (IsUnicorn(asset, now)) {
                _display.ShowUnicorn(asset);
                return;
            }

            asset.GetValue(now);

            portfolioValue = new MeasurableValue(portfolioValue.Get() + asset.Value.Get());
        }

        _display.ShowPortfolio(portfolioValue);
    }

    bool IsUnicorn(Asset asset, DateTime now) {
        if (asset.Description != "Unicorn") 
            return false;
        
        if (asset.Date.Subtract(now).TotalDays < 0) {
            return asset.Value.Get() > 0;
        }

        return true;

    }
}