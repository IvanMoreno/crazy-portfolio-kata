namespace Portfolio;

public class Portfolio
{
    private readonly PortfolioDisplay _display;
    readonly CsvAssetsLoader _assetsLoader;
    readonly RealtimeClock _clock;

    public Portfolio(PortfolioDisplay display, CsvAssetsLoader assetsLoader, RealtimeClock clock)
    {
        _assetsLoader = assetsLoader;
        _display = display;
        _clock = clock;
    }
    
    public void ComputePortfolioValue()
    {
        var assets = _assetsLoader.GetAllAssets(_clock.GetTime());
        if (assets.ContainsUnicorn) {
            _display.ShowUnicorn(assets.GetFirstUnicorn());
        }
        else {
            _display.ShowPortfolioValue(assets.TotalValue());
        }
    }
}