using Infrastructure.Data;

namespace Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }

  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}
