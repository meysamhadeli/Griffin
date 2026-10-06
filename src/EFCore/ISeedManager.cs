namespace Griffin.EFCore;

public interface ISeedManager
{
    Task ExecuteSeedAsync();
    Task ExecuteTestSeedAsync();
}