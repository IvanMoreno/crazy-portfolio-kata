using System.Collections;

namespace Portfolio;

public class AssetCollection : IEnumerable<Asset> {
    readonly IEnumerable<Asset> assets;
    readonly DateTime now;

    public AssetCollection(IEnumerable<Asset> assets, DateTime now) {
        this.assets = assets;
        this.now = now;
    }

    public bool ExistsUnicorn() {
        return assets.Any(asset => asset.IsUnicorn(now));
    }

    public Asset FirstUnicorn() {
        return assets.First(asset => asset.IsUnicorn(now));
    }

    public Value TotalValue() {
        return assets.Aggregate(Value.Measurable(0), (acc, asset) => acc.Add(asset.GetValue(now)));
    }

    public IEnumerator<Asset> GetEnumerator() {
        return assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}