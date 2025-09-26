using _Project.Scripts.Infrastructure.Systems.Components;
using Unity.AI.Navigation;
using UnityEngine;

namespace _Project.Scripts.Game.Features.Level.Components
{
  public class GroundComponent : MonoComponent<GroundComponent>
  {
    [SerializeField] private NavMeshSurface _navMeshSurface;

    public void BuildNavMesh() => _navMeshSurface.BuildNavMesh();
    public void UpdateNavMesh() => _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
  }
}