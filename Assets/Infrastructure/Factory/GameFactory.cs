﻿using Infrastructure.AssetManagement;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameObject HeroGameObject { get; set; }
    public event Action HeroCreated;

    private readonly IAssets _assets;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject CreateHero(GameObject at)
    {
      GameObject hero = InstantiateRegistered(AssetPaths.PlayerPath, at.transform.position.AddY(1));
      HeroGameObject = hero;
      HeroCreated?.Invoke();
      return hero;
    }

    public void CreateHud() =>
      InstantiateRegistered(AssetPaths.HudPath);

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }
    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath, at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
      {
        Register(progressReader);
      }
    }
    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }

      ProgressReaders.Add(progressReader);
    }
  }
}