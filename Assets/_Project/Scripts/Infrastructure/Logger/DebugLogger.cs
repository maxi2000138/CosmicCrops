﻿using _Project.Scripts.Infrastructure.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.Logger
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
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
      if (_staticDataService.LoggerConfig().IsLogTypeActive(logsType)) 
        Debug.Log(message);
    }
  }
}