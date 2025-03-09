using _Project.Scripts.Game.Entities.Character.Components;
using _Project.Scripts.Game.Infrastructure.StateMachine;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Game.Entities.Character.StateMachine.States
{
  public class CharacterStateDeath : CharacterState, IState
  {

    public CharacterStateDeath(IStateMachine stateMachine, CharacterComponent character) : base(stateMachine, character)
    {
    }


    void IState.Enter()
    {
      Character.UnitAnimator.OnDeath.Execute(R3.Unit.Default);
      
      Character.CleanSubscribe();
    }

    void IState.Exit() { }

    void IState.Tick() { }
  }
}