namespace Portfolio.Tests;

public class StubRepository : PortfolioRepository {
    public IEnumerable<Asset> Assets { get; set; }
    
    public IEnumerable<Asset> GetAssets() {
        return Assets;
    }
}