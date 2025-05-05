using _Project.Scripts.Game.Common.Data;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Weapon.Componets;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using R3;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Weapon.Systems
{
  public class BulletCollisionSystem : SystemComponent<BulletComponent>
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

    private void CheckEnemyCollision(BulletComponent bullet)
    {
      if(bullet.CollisionMask.Matches(CollisionLayer.Enemy) == false) return;

      for (int i = 0; i < _levelModel.Enemies.Count; i++)
      {
        bool targetIsAlive = _levelModel.Enemies[i].Health.IsAlive;
        bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

        if (targetIsAlive && isCollision)
        {
          Collision(bullet, _levelModel.Enemies[i]);
        }
      }
    }
    
    private void CheckCharacterCollision(BulletComponent bullet)
    {
      if(bullet.CollisionMask.Matches(CollisionLayer.Character) == false) return;
      
      bool targetIsAlive = _levelModel.Character.Health.IsAlive;
      bool isCollision = (bullet.Position - _levelModel.Character.Position).sqrMagnitude < bullet.CollisionDistance;

      if (targetIsAlive && isCollision)
      {
        Collision(bullet, _levelModel.Character);
      }
    }

    private void 
      Collision(BulletComponent bullet, ITarget target)
    {
      _abilityApplier.Apply(bullet.Ability, target);
      bullet.OnDestroy.Execute(Unit.Default);
    }
  }

}