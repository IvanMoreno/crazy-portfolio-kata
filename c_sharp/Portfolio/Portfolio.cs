namespace Portfolio;

public class RealtimeClock {
    public DateTime Now() {
        return DateTime.Now;
    }
}

public class Portfolio
{
    readonly Display _display;
    readonly PortfolioRepository _portfolioRepository;
    readonly RealtimeClock _clock;

    public Portfolio(Display display, PortfolioRepository portfolioRepository, RealtimeClock clock) {
        _portfolioRepository = portfolioRepository;
        _display = display;
        _clock = clock;
    }

    public void ComputePortfolioValue()
    {
        var now = _clock.Now();
        var assets = _portfolioRepository.GetAssets();

        foreach (var asset in assets) {
            if (asset.IsUnicorn(now)) {
                _display.ShowUnicorn(asset);
                return;
            }
        }

        _display.ShowPortfolio(PortfolioValue(assets, now));
    }

    static Value PortfolioValue(IEnumerable<Asset> assets, DateTime now) {
        return assets.Aggregate(Value.Measurable(0), (acc, asset) => acc.Add(asset.GetValue(now)));
    }
}