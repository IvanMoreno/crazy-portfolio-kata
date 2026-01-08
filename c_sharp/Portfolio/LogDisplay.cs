using System.Globalization;

namespace Portfolio;

public class LogDisplay : Display {
    public void ShowPortfolio(MeasurableValue portfolioValue) {
        Console.WriteLine(portfolioValue);
    }

    public void ShowUnicorn(Asset asset) {
        Console.WriteLine(
            "Portfolio is priceless because it got a unicorn on " +
            asset.Date.ToString(CultureInfo.CurrentCulture) + "!!!!!");
    }
}