using static System.Globalization.CultureInfo;

namespace Portfolio;

public class Portfolio
{
    private readonly string _portfolioCsvPath;
    private readonly PortfolioDisplay _portfolioDisplay;

    public Portfolio(string portfolioCsvPath, PortfolioDisplay portfolioDisplay)
    {
        _portfolioCsvPath = portfolioCsvPath;
        _portfolioDisplay = portfolioDisplay;
    }
    
    // Divergent change
    // Feature Envy
    // Special Case
    public void ComputePortfolioValue()
    {
        var allAssets = GetAllAssets();
        if (allAssets.Any(asset => asset.GetValue(DateTime.Now).IsPriceless)) {
            var pricelessAsset = allAssets.First(asset => asset.GetValue(DateTime.Now).IsPriceless);
            _portfolioDisplay.DisplayUnicorn(pricelessAsset);
            return;
        }

        _portfolioDisplay.Display(GetPortfolioValue(allAssets));
    }

    static AssetValue GetPortfolioValue(IEnumerable<Asset> allAssets) {
        return allAssets.Aggregate(new AssetValue(0), (current, asset) => current.Add(asset.GetValue(DateTime.Now)));
    }

    IEnumerable<Asset> GetAllAssets() {
        var readText = File.ReadAllText(_portfolioCsvPath);
        var lines = readText.Split(Environment.NewLine);
        return lines.Select(Parse);
    }

    static Asset Parse(string line) {
        var columns = line.Split(",");
        return new Asset(columns[0], DateTime.Parse(columns[1], CurrentCulture), new AssetValue(int.Parse(columns[2])));
    }
}