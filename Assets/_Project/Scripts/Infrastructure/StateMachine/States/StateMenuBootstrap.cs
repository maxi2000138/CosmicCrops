using System;
using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.Curtain;
using _Project.Scripts.Infrastructure.Factories.UI;
using _Project.Scripts.Infrastructure.StateMachine.States.Interfaces;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using R3;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
  public class StateMenuBootstrap : IEnterState, IExitState
  {
    private readonly IUIFactory _uiFactory;
    private readonly ILoadingCurtainService _loadingCurtain;

    private IGameStateMachine _gameStateMachine;
    private IDisposable _transitionDisposable;

    public StateMenuBootstrap(IUIFactory uiFactory, ILoadingCurtainService loadingCurtain)
    {
      _uiFactory = uiFactory;
      _loadingCurtain = loadingCurtain;
    }
    
    
    public async UniTask Enter(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      await SubscribeOnTransition();
      
      _loadingCurtain.Hide();
    }
    
    public UniTask Exit(IGameStateMachine gameStateMachine)
    { 
      _loadingCurtain.Show();

      _transitionDisposable?.Dispose();
      return UniTask.CompletedTask;
    }
    
    private async UniTask SubscribeOnTransition()
    {
      BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Menu);

      _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
    }

    private void ChangeState(Unit _) => _gameStateMachine.Enter<StateLoadGame>();
  }
}