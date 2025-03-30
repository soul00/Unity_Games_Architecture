﻿using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject CreateHero(GameObject at) => 
      _assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);

    public void CreateHud() => 
      _assets.Instantiate(AssetPaths.HudPath);
  }
}