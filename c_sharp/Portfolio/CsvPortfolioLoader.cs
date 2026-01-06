using System.Globalization;

namespace Portfolio;

public class AssetsValuation {
    readonly IEnumerable<Asset> assets;
    readonly DateTime date;
    public bool ContainsUnicorn => assets.Any(IsUnicorn);

    public AssetsValuation(IEnumerable<Asset> assets, DateTime date) {
        this.assets = assets;
        this.date = date;
    }
    
    public Asset GetFirstUnicorn() => assets.First(IsUnicorn);
    public AssetValue TotalValue() => assets.Aggregate(new AssetValue(0), (current, asset) => current.Add(asset.GetValue(date)));
    bool IsUnicorn(Asset asset) => asset.GetValue(date).IsUnicorn;
}

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
    
    public AssetsValuation GetAllAssets(DateTime now) {
        var readText = File.ReadAllText(_portfolioCsvPath);
        var lines = readText.Split(Environment.NewLine);
        return new(lines.Select(Parse), now);
    }

    static Asset Parse(string line) {
        var columns = line.Split(",");
        return new Asset(columns[0], DateTime.Parse(columns[1], CultureInfo.CurrentCulture), new AssetValue(int.Parse(columns[2])));
    }
}