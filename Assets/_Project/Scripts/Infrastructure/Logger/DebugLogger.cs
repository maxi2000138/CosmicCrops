using System.Diagnostics;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Utils;
using JetBrains.Annotations;
using VContainer.Unity;
using Debug = UnityEngine.Debug;

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

    [Conditional(Conditional.Logs)]
    public static void Log(string message, LogsType logsType, DebugColorType color = DebugColorType.Silver)
    {
      if (_staticDataService.LoggerPreset().IsLogTypeActive(logsType)) 
        Debug.Log($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }

    [Conditional(Conditional.Logs)]
    public static void LogWarning(string message, LogsType logsType, DebugColorType color = DebugColorType.Yellow)
    {
      if (_staticDataService.LoggerPreset().IsLogTypeActive(logsType)) 
        Debug.LogWarning($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }

    [Conditional(Conditional.Logs)]
    public static void LogError(string message, LogsType logsType, DebugColorType color = DebugColorType.Red)
    { 
      Debug.LogError($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }
  }
}