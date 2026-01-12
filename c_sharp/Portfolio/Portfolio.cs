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
        var assets = _portfolioRepository.GetAssets2(now);
        if (assets.ExistsUnicorn()) {
            _display.ShowUnicorn(assets.FirstUnicorn());
            return;
        }

        _display.ShowPortfolio(assets.TotalValue());
    }
}