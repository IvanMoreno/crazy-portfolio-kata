using static System.Globalization.CultureInfo;

namespace Portfolio;

public interface Display {
    void ShowPortfolio(MeasurableValue portfolioValue);
    void ShowUnicorn(Asset asset);
}

public class LogDisplay : Display {
    public void ShowPortfolio(MeasurableValue portfolioValue) {
        Console.WriteLine(portfolioValue);
    }

    public void ShowUnicorn(Asset asset) {
        Console.WriteLine(
            "Portfolio is priceless because it got a unicorn on " +
            asset.Date.ToString(CurrentCulture) + "!!!!!");
    }
}

public interface PortfolioRepository {
    IEnumerable<Asset> GetAssets();
}

public class CsvPortfolioRepository : PortfolioRepository {
    readonly string _portfolioCsvPath;

    public CsvPortfolioRepository(string portfolioCsvPath) {
        _portfolioCsvPath = portfolioCsvPath;
    }

    public IEnumerable<Asset> GetAssets() {
        var readText = File.ReadAllText(_portfolioCsvPath);
        var lines = readText.Split(Environment.NewLine);
        return lines.Select(Parse);
    }

    static Asset Parse(string line) {
        var columns = line.Split(",");
        return new Asset(columns[0],
            DateTime.Parse(columns[1], CurrentCulture),
            columns[0] == "Unicorn" ? new PricelessValue() : new MeasurableValue(int.Parse(columns[2])));
    }
}

public class Portfolio
{
    readonly Display _display;
    readonly CsvPortfolioRepository portfolioRepository;

    public Portfolio(Display display, CsvPortfolioRepository portfolioRepository) {
        this.portfolioRepository = portfolioRepository;
        _display = display;
    }

    public void ComputePortfolioValue()
    {
        var now = DateTime.Now;
        var portfolioValue = new MeasurableValue(0);
        
        foreach (var asset in portfolioRepository.GetAssets()) {
            if (asset.Date.Subtract(now).TotalDays < 0)
            {
                if (asset.Description != "French Wine")
                {
                    if (asset.Description != "Lottery Prediction")
                    {
                        if (asset.Value.Get() > 0)
                        {
                            if (asset.Description != "Unicorn")
                            {
                                asset.Value = new MeasurableValue(asset.Value.Get() - 20);
                            }
                            else
                            {
                                _display.ShowUnicorn(asset);
                                return;
                            }
                        }
                    }
                    else
                    {
                        asset.Value = new MeasurableValue(asset.Value.Get() - asset.Value.Get());
                    }
                }
                else
                {
                    if (asset.Value.Get() < 200) asset.Value = new MeasurableValue(asset.Value.Get() + 20);
                }
            }
            else
            {
                if (asset.Description != "French Wine" && asset.Description != "Lottery Prediction")
                {
                    if (asset.Value.Get() > 0.0)
                    {
                        if (asset.Description != "Unicorn")
                        {
                            asset.Value = new MeasurableValue(asset.Value.Get() - 10);
                        }
                        else
                        {
                            _display.ShowUnicorn(asset);
                            return;
                        }
                    }
                    else
                    {
                        if (asset.Description == "Unicorn") {
                            _display.ShowUnicorn(asset);
                            return;
                        }
                    }
                }
                else
                {
                    if (asset.Description == "Lottery Prediction")
                    {
                        if (asset.Value.Get() < 800)
                        {
                            asset.Value = new MeasurableValue(asset.Value.Get() + 5);

                            if (asset.Date.Subtract(now).TotalDays < 11)
                                if (asset.Value.Get() < 800)
                                    asset.Value = new MeasurableValue(asset.Value.Get() + 20);

                            if (asset.Date.Subtract(now).TotalDays < 6)
                                if (asset.Value.Get() < 800)
                                    asset.Value = new MeasurableValue(asset.Value.Get() + 100);
                        }
                    }
                    else
                    {
                        if (asset.Value.Get() < 200) asset.Value = new MeasurableValue(asset.Value.Get() + 10);
                    }
                }
            }

            portfolioValue = new MeasurableValue(portfolioValue.Get() + asset.Value.Get());
        }

        _display.ShowPortfolio(portfolioValue);
    }
}