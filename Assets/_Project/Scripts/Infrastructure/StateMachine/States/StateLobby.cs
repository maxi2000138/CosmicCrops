using System;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class StateLobby : IEnterState, IExitState
  {
    private readonly IUIFactory _uiFactory;
    
    private IDisposable _transitionDisposable;
    private IGameStateMachine _gameStateMachine;

    public StateLobby(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }
    
    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;

      await SubscribeOnTransition();
    }

    public UniTask Exit(IGameStateMachine gameStateMachine)
    { 
      _transitionDisposable?.Dispose();
      return UniTask.CompletedTask;
    }
    
    private async UniTask SubscribeOnTransition()
    {
      BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Lobby);

      _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
    }

    private void ChangeState(Unit _) => _gameStateMachine.Enter<StateGame>();
  }


}