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
        var portfolioValue = new AssetValue(0);

        foreach (var asset in GetAllAssets()) {
            // Special case
            var assetValue = asset.GetValue(DateTime.Now);
            if (assetValue.IsPriceless) {
                Console.WriteLine(
                    "Portfolio is priceless because it got a unicorn on " +
                    asset.Date.ToString(CurrentCulture) + "!!!!!");
                return;
            }
            
            portfolioValue = portfolioValue.Add(assetValue);
        }

        _portfolioDisplay.Display(portfolioValue);
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