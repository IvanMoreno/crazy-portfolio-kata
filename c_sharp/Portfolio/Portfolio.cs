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
        var now = DateTime.Now;
        var readText = File.ReadAllText(_portfolioCsvPath);
        var lines = readText.Split(Environment.NewLine);
        var portfolioValue = new MeasurableValue(0);
        var assets = lines.Select(Parse);

        foreach (var asset in assets) {
            // Special case
            if (asset.Description == "Unicorn") {
                Console.WriteLine(
                    "Portfolio is priceless because it got a unicorn on " +
                    asset.Date.ToString(CurrentCulture) + "!!!!!");
                return;
            }

            portfolioValue = portfolioValue.Add(asset.GetValue(now));
        }

        _portfolioDisplay.Display(portfolioValue);
    }

    static Asset Parse(string line) {
        var columns = line.Split(",");
        
        return new Asset(columns[0],
            DateTime.Parse(columns[1], CurrentCulture),
            columns[0] == "Unicorn" ? new PricelessValue() : new MeasurableValue(int.Parse(columns[2])));
    }
}