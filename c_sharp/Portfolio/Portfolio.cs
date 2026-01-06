namespace Portfolio;

public class Portfolio
{
    private readonly PortfolioDisplay _portfolioDisplay;
    readonly CsvAssetsLoader _assetsLoader;

    public Portfolio(PortfolioDisplay portfolioDisplay, CsvAssetsLoader assetsLoader)
    {
        _assetsLoader = assetsLoader;
        _portfolioDisplay = portfolioDisplay;
    }
    
    public void ComputePortfolioValue()
    {
        var assets = _assetsLoader.GetAllAssets(DateTime.Now);
        if (assets.ContainsUnicorn) {
            _portfolioDisplay.DisplayUnicorn(assets.GetFirstUnicorn());
        }
        else {
            _portfolioDisplay.Display(assets.TotalValue());
        }
    }
}