namespace Portfolio;

public interface PortfolioRepository {
    AssetCollection GetAssets(DateTime now);
}