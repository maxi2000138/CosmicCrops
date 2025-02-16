using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Logger;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StaticData.Data
{
  [CreateAssetMenu(fileName = nameof(LoggerConfig), menuName = "Config/" + nameof(LoggerConfig))]
  public sealed class LoggerConfig : SerializedScriptableObject
  {
    [DictionaryDrawerSettings(IsReadOnly = true, DisplayMode = DictionaryDisplayOptions.OneLine)]
    [SerializeField]
    private Dictionary<LogsType, bool> _logsActive = new Dictionary<LogsType, bool>();

    private void OnEnable()
    {
      foreach (LogsType logType in Enum.GetValues(typeof(LogsType))) 
        _logsActive.TryAdd(logType, true);
    }

    public bool IsLogTypeActive(LogsType logsType)
    {
      return _logsActive.TryGetValue(logsType, out bool isActive) && isActive;
    }
  }
}