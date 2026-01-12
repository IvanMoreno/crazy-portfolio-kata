namespace Portfolio;

public interface PortfolioRepository {
    IEnumerable<Asset> GetAssets();
    AssetCollection GetAssets2(DateTime now);
}