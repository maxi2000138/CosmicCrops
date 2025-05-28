using System.Collections;
using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Utils.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Game.Entities.Enemy.Components
{
  public class EnemySpawnerComponent : MonoComponent<EnemySpawnerComponent>
  { 
    [ValueDropdown(nameof(GetAllEnemies))]
    [SerializeField] private string _enemy;

    public Vector3 Position => transform.position;
    public string Enemy => _enemy;

    
    private static IEnumerable GetAllEnemies()
    {
      return ConfigsUtils.GetAllUnemiesNames();
    }
  }
}