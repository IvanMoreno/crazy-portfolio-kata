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
        var portfolioValue = Value.Measurable(0);
        
        foreach (var asset in _portfolioRepository.GetAssets()) {
            if (asset.IsUnicorn(now)) {
                _display.ShowUnicorn(asset);
                return;
            }

            portfolioValue = portfolioValue.Add(asset.GetValue(now));
        }

        _display.ShowPortfolio(portfolioValue);
    }
}