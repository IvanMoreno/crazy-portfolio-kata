namespace Portfolio;

public class Portfolio
{
    private readonly PortfolioDisplay _display;
    readonly CsvAssetsLoader _assetsLoader;

    public Portfolio(PortfolioDisplay display, CsvAssetsLoader assetsLoader)
    {
        _assetsLoader = assetsLoader;
        _display = display;
    }
    
    public void ComputePortfolioValue()
    {
        var assets = _assetsLoader.GetAllAssets(DateTime.Now);
        if (assets.ContainsUnicorn) {
            _display.ShowUnicorn(assets.GetFirstUnicorn());
        }
        else {
            _display.ShowPortfolioValue(assets.TotalValue());
        }
    }
}