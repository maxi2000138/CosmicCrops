﻿using _Project.Scripts._Infrastructure.StaticData;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.Services.Logger
{
  public class DebugLogger : IInitializable
  {
    private static IStaticDataService _staticDataService;

    public DebugLogger(IStaticDataService staticDataService)
    {
      _staticDataService = staticDataService;
    }
    public void Initialize() { }
    
    public static void Log(string message, LogsType logsType)
    {
      if (_staticDataService.LoggerData().IsLogTypeActive(logsType)) 
        Debug.Log(message);
    }
  }
}