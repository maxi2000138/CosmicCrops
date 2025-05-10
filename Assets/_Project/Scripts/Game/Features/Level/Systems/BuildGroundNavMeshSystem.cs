using _Project.Scripts.Game.Features.Level.Components;
using _Project.Scripts.Infrastructure.Systems;

namespace _Project.Scripts.Game.Features.Level.Systems
{
  public class BuildGroundNavMeshSystem : SystemComponent<GroundComponent>
  {
    protected override void OnEnableComponent(GroundComponent armament)
    {
      base.OnEnableComponent(armament);
      
      armament.BuildNavMesh();
    }
  }
}