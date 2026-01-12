namespace Portfolio.Tests;

public class StubRepository : PortfolioRepository {
    public IEnumerable<Asset> Assets { get; set; }
    
    public AssetCollection GetAssets(DateTime now) {
        return new(Assets, now);
    }
}