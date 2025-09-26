using System;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.Progress;
using _Project.Scripts.Infrastructure.StateMachine.Data;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class StateGameResult : IOverloadedEnterState<GameResult>, IExitState
  {
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IProgressService _progressService;
    private readonly IUIFactory _uiFactory;
    private IGameStateMachine _gameStateMachine;

    private IDisposable _transitionDisposable;

    public StateGameResult(IUIFactory uiFactory, ILoadingCurtainService loadingCurtain, IProgressService progressService)
    {
      _loadingCurtain = loadingCurtain;
      _progressService = progressService;
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
        _progressService.LevelData.Data.Value++;
        
        screen = await _uiFactory.CreateScreen(ScreenType.Win);
      }
      else if (gameResult == GameResult.Loose)
      {
        screen = await _uiFactory.CreateScreen(ScreenType.Loose);
      }
      
      _transitionDisposable = screen!.CloseScreen.First().Subscribe(ChangeState);
    }
    
    private void ChangeState(Unit _) => _gameStateMachine.Enter<StateLoadMenu>();
  }
}