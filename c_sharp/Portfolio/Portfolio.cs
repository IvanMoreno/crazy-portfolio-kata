using static System.Globalization.CultureInfo;

namespace Portfolio;

public class CsvPortfolioLoader {
    readonly string _portfolioCsvPath;

    public CsvPortfolioLoader(string portfolioCsvPath) {
        _portfolioCsvPath = portfolioCsvPath;
    }

    public IEnumerable<Asset> GetAllAssets() {
        var readText = File.ReadAllText(_portfolioCsvPath);
        var lines = readText.Split(Environment.NewLine);
        return lines.Select(Parse);
    }

    static Asset Parse(string line) {
        var columns = line.Split(",");
        return new Asset(columns[0], DateTime.Parse(columns[1], CurrentCulture), new AssetValue(int.Parse(columns[2])));
    }
}

public class Portfolio
{
    private readonly PortfolioDisplay _portfolioDisplay;
    readonly CsvPortfolioLoader portfolioLoader;

    public Portfolio(string portfolioCsvPath, PortfolioDisplay portfolioDisplay, CsvPortfolioLoader portfolioLoader)
    {
        this.portfolioLoader = portfolioLoader;
        _portfolioDisplay = portfolioDisplay;
    }
    
    public void ComputePortfolioValue()
    {
        var allAssets = portfolioLoader.GetAllAssets();
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