using _Project.Scripts.Infrastructure.StaticData;
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
    
    public static void Log(string message, LogsType logsType, DebugColorType color = DebugColorType.Silver)
    {
      if (_staticDataService.LoggerConfig().IsLogTypeActive(logsType)) 
        Debug.Log($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }
    
    public static void LogWarning(string message, LogsType logsType, DebugColorType color = DebugColorType.Yellow)
    {
      if (_staticDataService.LoggerConfig().IsLogTypeActive(logsType)) 
        Debug.LogWarning($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }
    
    public static void LogError(string message, LogsType logsType, DebugColorType color = DebugColorType.Red)
    { 
      Debug.LogError($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }
  }
}