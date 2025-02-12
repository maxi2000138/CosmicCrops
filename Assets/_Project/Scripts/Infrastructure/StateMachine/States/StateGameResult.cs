using System;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.StateMachine.Data;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.UI.Screens;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using VContainer;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class StateGameResult : IOverloadedEnterState<GameResult>, IExitState
  {
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IUIFactory _uiFactory;
    private IGameStateMachine _gameStateMachine;

    private IDisposable _transitionDisposable;

    public StateGameResult(IUIFactory uiFactory, ILoadingCurtainService loadingCurtain)
    {
      _loadingCurtain = loadingCurtain;
      _uiFactory = uiFactory;
    }

    public UniTask Enter(IGameStateMachine gameStateMachine, GameResult gameResult)
    {
      _gameStateMachine = gameStateMachine;
      SubscribeOnTransition(gameResult).Forget();
      
      return UniTask.CompletedTask;
    }
    
    public UniTask Exit(IGameStateMachine gameStateMachine)
    {
      _transitionDisposable?.Dispose();
      _loadingCurtain.Show();
      
      return UniTask.CompletedTask;
    }
    
    private async UniTaskVoid SubscribeOnTransition(GameResult gameResult)
    {
      BaseScreen screen = null;
      
      if (gameResult == GameResult.Win)
      {
        screen = await _uiFactory.CreateScreen(ScreenType.Win);
      }
      
      _transitionDisposable = screen!.CloseScreen.First().Subscribe(ChangeState);
    }
    
    private void ChangeState(Unit _) => _gameStateMachine.Enter<StateMenu>();
  }
}