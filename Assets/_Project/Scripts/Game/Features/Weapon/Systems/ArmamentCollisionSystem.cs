using _Project.Scripts.Game.Common.Data;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class ArmamentCollisionSystem : SystemComponent<ArmamentComponent>
  {
    private LevelModel _levelModel;
    private IAbilityApplier _abilityApplier;

    [Inject]
    private void Construct(LevelModel levelModel, IAbilityApplier abilityApplier)
    {
      _abilityApplier = abilityApplier;
      _levelModel = levelModel;
    }
    
    protected override void OnUpdate()
    {
      base.OnUpdate();

      Components.Foreach(CheckEnemyCollision);
      Components.Foreach(CheckCharacterCollision);
    }

    private void CheckEnemyCollision(ArmamentComponent armament)
    {
      if(armament.CollisionMask.Matches(CollisionLayer.Enemy) == false) return;

      for (int i = 0; i < _levelModel.Enemies.Count; i++)
      {
        bool targetIsAlive = _levelModel.Enemies[i].Health.IsAlive;
        bool isCollision = (armament.Position - _levelModel.Enemies[i].Position).sqrMagnitude < armament.CollisionSqrDistance;

        if (targetIsAlive && isCollision)
        {
          Collision(armament, _levelModel.Enemies[i]);
        }
      }
    }
    
    private void CheckCharacterCollision(ArmamentComponent armament)
    {
      if(armament.CollisionMask.Matches(CollisionLayer.Player) == false) return;
      
      bool targetIsAlive = _levelModel.Character.Health.IsAlive;
      bool isCollision = (armament.Position - _levelModel.Character.Position).sqrMagnitude < armament.CollisionSqrDistance;

      if (targetIsAlive && isCollision)
      {
        Collision(armament, _levelModel.Character);
      }
    }

    private void 
      Collision(ArmamentComponent bullet, ITarget target)
    {
      _abilityApplier.Apply(bullet.Ability, target);
      bullet.OnDestroy.Execute(Unit.Default);
    }
  }

}