﻿  using _Project.Scripts.Game.Loot.Data;
  using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Loot.Components
{
  public class LootSpawnerComponent : MonoComponent<LootSpawnerComponent>
  {
    [SerializeField] private LootType _lootType;
    
    public LootType LootType => _lootType;
    public Vector3 Position => transform.position;
  }
}