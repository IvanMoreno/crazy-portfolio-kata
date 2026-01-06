namespace Portfolio;

public class Portfolio
{
    private readonly PortfolioDisplay _display;
    readonly CsvAssetsLoader _assetsLoader;
    readonly RealtimeClock realtimeClock;

    public Portfolio(PortfolioDisplay display, CsvAssetsLoader assetsLoader)
    {
        _assetsLoader = assetsLoader;
        _display = display;
        realtimeClock = new RealtimeClock();
    }
    
    public void ComputePortfolioValue()
    {
        var assets = _assetsLoader.GetAllAssets(realtimeClock.GetTime());
        if (assets.ContainsUnicorn) {
            _display.ShowUnicorn(assets.GetFirstUnicorn());
        }
        else {
            _display.ShowPortfolioValue(assets.TotalValue());
        }
    }
}