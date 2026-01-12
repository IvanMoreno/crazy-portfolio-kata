namespace Portfolio;

public interface PortfolioRepository {
    AssetCollection GetAssets2(DateTime now);
}