using System;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game.Features.Abilities._Configs
{
  [Serializable]
  public class AbilityData
  {
    public string Id;
    
    public string[] Effects;
    public string[] Statuses;
  }
}