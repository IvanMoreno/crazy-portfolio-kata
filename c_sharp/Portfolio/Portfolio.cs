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