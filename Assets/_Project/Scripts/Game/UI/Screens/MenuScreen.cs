using _Project.Scripts.Utils;
using _Project.Scripts.Utils.Constants;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;

namespace _Project.Scripts.Game.UI.Screens
{
  public class MenuScreen : BaseScreen
  {
    private Tween _tween;

    protected override void OnEnable()
    {
      base.OnEnable();

      _button
        .OnClickAsObservable()
        .ThrottleFirst(ButtonSettings.ClickThrottle)
        .Subscribe(_ => Hide().Forget())
        .AddTo(LifeTimeDisposable);
      
      Show().Forget();
    }
    
    protected override async UniTask Show()
    {
      await base.Show();
            
      _tween = BounceButton();
    }

    
    public override ScreenType GetScreenType() => ScreenType.Menu;
    
    protected override async UniTask Hide()
    {
      _tween?.Kill();
      
      await base.Hide();

      CloseScreen.Execute(Unit.Default);
    }
  }
}