﻿using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Units.Character.Components
{
  public class CharacterSpawnerComponent : MonoComponent<CharacterSpawnerComponent>
  {
    public Vector3 Position => transform.position;
  }
}