using _Project.Scripts._Infrastructure.MVVM.UI;
using R3;

namespace _Project.Scripts.Gameplay.UI.Screen
{
  public class ScreenGameplayViewModel : WindowViewModel
  {
    private readonly GameplayUIRouter _uiRouter;
    private readonly Subject<Unit> _exitSceneRequest;
    
    public override string Id => "ScreenGameplay";

    public ScreenGameplayViewModel(GameplayUIRouter uiRouter, Subject<Unit> exitSceneRequest)
    {
      _uiRouter = uiRouter;
      _exitSceneRequest = exitSceneRequest;
    }
    
    public void RequestOpenPopupA()
    {
      _uiRouter.OpenPopupA();
    }
    
    public void RequestGoToMainMenu()
    {
      _exitSceneRequest.OnNext(Unit.Default);
    }

  }
}