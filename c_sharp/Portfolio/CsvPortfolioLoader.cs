using System.Globalization;

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
        return new Asset(columns[0], DateTime.Parse(columns[1], CultureInfo.CurrentCulture), new AssetValue(int.Parse(columns[2])));
    }
}