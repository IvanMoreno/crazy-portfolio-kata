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
        var allAssets = _portfolioLoader.GetAllAssets();
        if (allAssets.Any(IsUnicorn)) {
            _portfolioDisplay.DisplayUnicorn(allAssets.First(IsUnicorn));
        }
        else {
            _portfolioDisplay.Display(GetPortfolioValue(allAssets));
        }
    }

    static bool IsUnicorn(Asset asset) => asset.GetValue(DateTime.Now).IsPriceless;

    static AssetValue GetPortfolioValue(IEnumerable<Asset> allAssets) {
        return allAssets.Aggregate(new AssetValue(0), (current, asset) => current.Add(asset.GetValue(DateTime.Now)));
    }
}