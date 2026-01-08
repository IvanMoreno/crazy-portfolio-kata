namespace Portfolio;

public interface PortfolioRepository {
    IEnumerable<Asset> GetAssets();
}