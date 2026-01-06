using static System.Globalization.CultureInfo;
namespace Portfolio;

public class LogDisplay : PortfolioDisplay {
    public void ShowPortfolioValue(AssetValue portfolioValue) {
        Console.WriteLine(portfolioValue);
    }

    public void ShowUnicorn(Asset asset) {
        Console.WriteLine(
            "Portfolio is priceless because it got a unicorn on " +
            asset.Date.ToString(CurrentCulture) + "!!!!!");
    }
}