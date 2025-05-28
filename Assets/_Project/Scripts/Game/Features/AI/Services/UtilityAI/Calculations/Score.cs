using System;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Enemy.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations
  {
    public class Score
    {
      public float AsIs(float input, IEnemy enemy)
      {
        return input;
      }
      
      public Func<float, EnemyComponent, float> ScaleBy(float scale)
      {
        return (input, _) => input * scale;
      }
      
      public Func<float, EnemyComponent, float> ScaleInvertedValueBy(float scale)
      {
        return (input, _) => 
        {
          if (Mathf.Approximately(input, 0f)) return float.PositiveInfinity;
          
          return 1f / input * scale;
        };
      }
      
      public Func<float, EnemyComponent, float> CullByPercentageHealth(float scale)
      {
        return (input, _) => 
        {
          if (Mathf.Approximately(input, 0f)) return float.PositiveInfinity;
          
          return (1f / input - 1f) * scale;
        };
      }
    }
  }