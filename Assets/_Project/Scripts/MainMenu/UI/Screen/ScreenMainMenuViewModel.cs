using _Project.Scripts.Infrastructure.MVVM.UI;
using _Project.Scripts.MainMenu.UI;
using R3;

namespace _Project.Scripts.Gameplay.UI.Screen
{
  public class ScreenMainMenuViewModel : WindowViewModel
  {
    private readonly MainMenuUIRouter _uiRouter;
    private readonly Subject<Unit> _exitSceneRequest;
    
    public override string Id => "ScreenMainMenu";

    public ScreenMainMenuViewModel(MainMenuUIRouter uiRouter, Subject<Unit> exitSceneRequest)
    {
      _uiRouter = uiRouter;
      _exitSceneRequest = exitSceneRequest;
    }
    
    public void RequestGoToGameplay()
    {
      _exitSceneRequest.OnNext(Unit.Default);
    }
  }
}