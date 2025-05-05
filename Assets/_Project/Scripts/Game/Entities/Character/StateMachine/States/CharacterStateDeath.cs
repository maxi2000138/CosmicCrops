using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
  public class CharacterStateDeath : CharacterState, IUnitState
  {

    public CharacterStateDeath(IUnitStateMachine unitStateMachine, CharacterComponent character) : base(unitStateMachine, character)
    {
    }


    void IUnitState.Enter()
    {
      Character.UnitAnimator.OnDeath.Execute(R3.Unit.Default);
      
      Character.CleanSubscribe();
    }

    void IUnitState.Exit() { }

    void IUnitState.Tick() { }
  }
}