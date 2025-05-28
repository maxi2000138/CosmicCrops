using _Project.Scripts.Game.UI.Screens;
using _Project.Scripts.Infrastructure.GUI;
using _Project.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using VContainer;

namespace CodeBase.UI.Screens
{
  public sealed class SettingsScreen : BaseScreen
  {
    private IGuiService _guiService;
        
    private Tween _tween;

    [Inject]
    private void Construct(IGuiService guiService)
    {
      _guiService = guiService;
    }

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
        
    public override ScreenType GetScreenType() => ScreenType.Settings;

    protected override async UniTask Show()
    {
      await base.Show();
            
      _tween = BounceButton();
    }

    protected override async UniTask Hide()
    {
      _tween?.Kill();
            
      await base.Hide();
            
      await FadeCanvas(1f, 0f).AsyncWaitForCompletion().AsUniTask();
            
      _guiService.Pop();
            
      CloseScreen.Execute(Unit.Default);
    }
  }
}