using _Project.Scripts._Infrastructure.ComponentSystemsCore.Components;
using UnityEngine;

namespace _Project.Scripts.Game.Character.Components
{
  public class CharacterSpawnerComponent : MonoComponent<CharacterSpawnerComponent>
  {
    public Vector3 Position => transform.position;
  }
}