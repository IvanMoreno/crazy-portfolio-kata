namespace Portfolio;

public class Portfolio
{
    readonly Display _display;
    readonly PortfolioRepository _portfolioRepository;

    public Portfolio(Display display, PortfolioRepository portfolioRepository) {
        _portfolioRepository = portfolioRepository;
        _display = display;
    }

    public void ComputePortfolioValue()
    {
        var now = DateTime.Now;
        var portfolioValue = new MeasurableValue(0);
        
        foreach (var asset in _portfolioRepository.GetAssets()) {
            if (IsUnicorn(asset, now)) {
                _display.ShowUnicorn(asset);
                return;
            }
            
            if (GetValue(asset, now)) return;

            portfolioValue = new MeasurableValue(portfolioValue.Get() + asset.Value.Get());
        }

        _display.ShowPortfolio(portfolioValue);
    }

    bool GetValue(Asset asset, DateTime now) {
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
                        else {
                            _display.ShowUnicorn(asset);
                            return true;
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
                    else {
                        _display.ShowUnicorn(asset);
                        return true;
                    }
                }
                else
                {
                    if (asset.Description == "Unicorn") {
                        _display.ShowUnicorn(asset);
                        return true;
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

        return false;
    }
    
    bool IsUnicorn(Asset asset, DateTime now) {
        if (asset.Description != "Unicorn") 
            return false;
        
        if (asset.Date.Subtract(now).TotalDays < 0) {
            return asset.Value.Get() > 0;
        }

        return true;

    }
}