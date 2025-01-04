using _Project.Scripts.Gameplay.UI.Screen;
using _Project.Scripts.Infrastructure.MVVM.UI;
using _Project.Scripts.Infrastructure.UI;
using R3;
using VContainer;

namespace _Project.Scripts.MainMenu.UI
{
  public class MainMenuUIRouter : UIRouter
  {
    private readonly Subject<Unit> _exitSceneRequest;
    
    public MainMenuUIRouter(IObjectResolver objectResolver) : base(objectResolver)
    {
      _exitSceneRequest = objectResolver.Resolve<SubjectsService>().ExitSceneRequest;
    }
    
    public ScreenMainMenuViewModel OpenScreenMainMenu()
    {
      var viewModel = new ScreenMainMenuViewModel(this, _exitSceneRequest);
      var rootUI = ObjectResolver.Resolve<MainMenuUIRootViewModel>();

      rootUI.OpenScreen(viewModel);

      return viewModel;
    }
  }
}