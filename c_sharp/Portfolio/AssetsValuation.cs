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