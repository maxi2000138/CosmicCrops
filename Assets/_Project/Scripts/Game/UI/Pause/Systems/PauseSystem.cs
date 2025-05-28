using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Pause.Components;
using _Project.Scripts.Infrastructure.Systems;
using R3;
using VContainer;

namespace _Project.Scripts.Game.Features.Pause.Systems
{
  public class PauseSystem : SystemComponent<PauseComponent>
  {
    private LevelModel _levelModel;

    [Inject]
    private void Construct(LevelModel levelModel)
    {
      _levelModel = levelModel;
    }

    protected override void OnEnableComponent(PauseComponent component)
    {
      base.OnEnableComponent(component);

      _levelModel.OnPause
        .Subscribe(component.Pause)
        .AddTo(component.LifetimeDisposable);
    }
  }
}