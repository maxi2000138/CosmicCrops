using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;

namespace _Project.Scripts.Game.UI.Screens
{
  public sealed class WinScreen : BaseScreen
  {
    private Tween _tween;

    protected override void OnEnable()
    {
      base.OnEnable();
            
      _button
        .OnClickAsObservable()
        .First()
        .Subscribe(_ => Hide().Forget())
        .AddTo(LifeTimeDisposable);
            
      Show().Forget();
    }
        
    public override ScreenType GetScreenType() => ScreenType.Win;

    protected override async UniTask Show()
    {
      await base.Show();

      _tween = BounceButton();
    }

    protected override async UniTask Hide()
    {
      _tween?.Kill();

      await base.Hide();
            
      CloseScreen.Execute(Unit.Default);
    }
  }

}