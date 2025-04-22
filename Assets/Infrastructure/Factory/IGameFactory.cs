using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

    GameObject CreateHero(GameObject at);
    void Cleanup();
    void CreateHud();
  }
}