using System;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.SceneLoader;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using R3;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public class StateMenu : IEnterState, IExitState
  {
    private readonly ISceneLoaderService _sceneLoaderService;
    private readonly ILoadingCurtainService _loadingCurtain;
    private readonly IUIFactory _uiFactory;

    private IDisposable _transitionDisposable;
    private IGameStateMachine _gameStateMachine;

    public StateMenu(IUIFactory uiFactory, ISceneLoaderService sceneLoaderService, ILoadingCurtainService loadingCurtain)
    {
      _sceneLoaderService = sceneLoaderService;
      _loadingCurtain = loadingCurtain;
      _uiFactory = uiFactory;
    }
    
    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      
      await _sceneLoaderService.Load(Scenes.MENU);
      await SubscribeOnTransition();
      
      _loadingCurtain.Hide();
    }

    public UniTask Exit(IGameStateMachine gameStateMachine)
    { 
      _transitionDisposable?.Dispose();
      return UniTask.CompletedTask;
    }
    
    private async UniTask SubscribeOnTransition()
    {
      BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Menu);

      _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
    }

    private void ChangeState(Unit _) => _gameStateMachine.Enter<StateLoadGameScene>();
  }
}