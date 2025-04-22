﻿using Infrastructure.Data;

namespace Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    PlayerProgress LoadProgress();
    void SaveProgress();
  }
}