using _Project.Scripts._Infrastructure.MVVM.UI;
using _Project.Scripts._Infrastructure.UI;
using _Project.Scripts.Gameplay.UI.PopupA;
using _Project.Scripts.Gameplay.UI.Screen;
using R3;
using VContainer;

namespace _Project.Scripts.Gameplay.UI
{
  public class GameplayUIRouter : UIRouter
  {
    private readonly Subject<Unit> _exitSceneRequest;
    
    public GameplayUIRouter(IObjectResolver objectResolver) : base(objectResolver)
    {
      _exitSceneRequest = objectResolver.Resolve<SubjectsService>().ExitSceneRequest;
    }
    
    public ScreenGameplayViewModel OpenScreenGameplay()
    {
      var viewModel = new ScreenGameplayViewModel(this, _exitSceneRequest);
      var rootUI = ObjectResolver.Resolve<GameplayUIRootViewModel>();

      rootUI.OpenScreen(viewModel);

      return viewModel;
    }
    
    public PopupAViewModel OpenPopupA()
    {
      var a = new PopupAViewModel();
      var rootUI = ObjectResolver.Resolve<GameplayUIRootViewModel>();

      rootUI.OpenPopup(a);

      return a;
    }
  }
}