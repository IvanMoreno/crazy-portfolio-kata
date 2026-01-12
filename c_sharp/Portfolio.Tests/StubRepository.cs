namespace Portfolio.Tests;

public class StubRepository : PortfolioRepository {
    public IEnumerable<Asset> Assets { get; set; }
    
    public IEnumerable<Asset> GetAssets() {
        return Assets;
    }

    public AssetCollection GetAssets2(DateTime now) {
        return new(Assets, now);
    }
}