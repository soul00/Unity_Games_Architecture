﻿using System;

namespace Infrastructure.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;

    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
    }
  }
}
