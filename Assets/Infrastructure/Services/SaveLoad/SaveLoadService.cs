using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
  class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gamefactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gamefactory)
    {
      _progressService = progressService;
      _gamefactory = gamefactory;
    }

    public void SaveProgress()
    {
      foreach (var progressWriter in _gamefactory.ProgressWriters)
      {
        progressWriter.UpdateProgress(_progressService.Progress);

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
      }
    }
    public PlayerProgress LoadProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

  }
}