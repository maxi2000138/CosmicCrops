using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Enemy.Actions;
using _Project.Scripts.Game.Entities.Enemy.Components;
using _Project.Scripts.Game.Entities.Enemy.StateMachine.States;
using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using _Project.Scripts.Game.Features.Abilities.Services;
using UnityEngine;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations
{
  public class When
  {
    private readonly IAbilityStatsProvider _abilityStatsProvider;
    
    public When(IAbilityStatsProvider abilityStatsProvider)
    {
      _abilityStatsProvider = abilityStatsProvider;
    }
    
    public bool ActionIsPatrol(BattleAction battleAction, EnemyComponent enemy)
    {
      return battleAction.ActionType == UnitActionType.Patrol;
    }
    
    public bool ActionIsPursuit(BattleAction battleAction, EnemyComponent enemy)
    {
      return battleAction.ActionType == UnitActionType.Pursuit;
    }

    public bool ActionIsFight(BattleAction battleAction, EnemyComponent enemy)
    {
      return battleAction.ActionType == UnitActionType.Fight;
    }

    public bool TargetInPursuitRange(BattleAction battleAction, EnemyComponent enemy)
    {
      float distanceToTarget = DistanceToTarget(battleAction, enemy);
      return distanceToTarget <= enemy.Stats.PursuitRadius;
    }
    
    public bool TargetInAttackRange(BattleAction battleAction, EnemyComponent enemy)
    {
      float distanceToTarget = DistanceToTarget(battleAction, enemy);
      var attackDistance = enemy.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
      return distanceToTarget <= attackDistance;
    }


    public bool TargetIsEnemy(BattleAction battleAction, EnemyComponent enemy)
    {
      return battleAction.Unit is IEnemy;
    }
    
    public bool TargetIsCharacter(BattleAction battleAction, EnemyComponent enemy)
    {
      return battleAction.Unit is ICharacter;
    }
    
    public bool WeaponDamaging(BattleAction battleAction, EnemyComponent enemy)
    {
      var abilityStats = _abilityStatsProvider.GetAbilityStats(enemy.WeaponMediator.CurrentWeapon.Weapon.Ability());
      return abilityStats.EffectSetups.Any(x => x.EffectTypeId == EffectTypeId.Damage);
    }
    
    public bool WeaponHealing(BattleAction battleAction, EnemyComponent enemy)
    {
      return IsWeaponHealing(enemy);
    }

    public bool TargetWantsToHeal(BattleAction battleAction, EnemyComponent enemy)
    {
      return (battleAction.Unit.StateMachine.UnitStateMachine.CurrentState is EnemyStatePursuit  ||
             battleAction.Unit.StateMachine.UnitStateMachine.CurrentState is EnemyStateFight)
             && battleAction.Unit.Target == enemy
             && IsWeaponHealing(battleAction.Unit);
    }

    public float DistanceToTarget(BattleAction battleAction, EnemyComponent enemy)
    {
      return Vector3.Distance(enemy.Position, battleAction.Unit.Position);
    }
    
    private bool IsWeaponHealing(IUnit unit)
    {
      var abilityStats = _abilityStatsProvider.GetAbilityStats(unit.WeaponMediator.CurrentWeapon.Weapon.Ability());
      return abilityStats.EffectSetups.Any(x => x.EffectTypeId == EffectTypeId.Heal);
    }
  }

}