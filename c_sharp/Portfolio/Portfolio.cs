namespace Portfolio;

public class Portfolio
{
    private readonly PortfolioDisplay _portfolioDisplay;
    readonly CsvPortfolioLoader _portfolioLoader;

    public Portfolio(PortfolioDisplay portfolioDisplay, CsvPortfolioLoader portfolioLoader)
    {
        _portfolioLoader = portfolioLoader;
        _portfolioDisplay = portfolioDisplay;
    }
    
    public void ComputePortfolioValue()
    {
        var assets = _portfolioLoader.GetAllAssets(DateTime.Now);
        if (assets.ContainsUnicorn) {
            _portfolioDisplay.DisplayUnicorn(assets.GetFirstUnicorn());
        }
        else {
            _portfolioDisplay.Display(assets.TotalValue());
        }
    }

    static bool IsUnicorn(Asset asset) => asset.GetValue(DateTime.Now).IsUnicorn;

    static AssetValue GetPortfolioValue(IEnumerable<Asset> allAssets) {
        return allAssets.Aggregate(new AssetValue(0), (current, asset) => current.Add(asset.GetValue(DateTime.Now)));
    }
}