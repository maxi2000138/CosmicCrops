using System;
using _Project.Scripts.Game.Features;
using VContainer.Unity;

namespace _Project.Scripts.Game.Infrastructure.Systems
{
  public class SystemsContainer : IInitializable ,ITickable, IFixedTickable, ILateTickable, IDisposable
  {
    private readonly BattleFeature _battleFeature;

    public SystemsContainer(BattleFeature battleFeature)
    {
      _battleFeature = battleFeature;
    }
    
    public void Initialize()
    {
      _battleFeature.EnableSystems();
    }
    
    void ITickable.Tick()
    { 
      _battleFeature.Update();
    }
    void IFixedTickable.FixedTick()
    { 
      _battleFeature.FixedUpdate();
    }
    void ILateTickable.LateTick()
    {
        _battleFeature.LateUpdate();
    }

    void IDisposable.Dispose()
    {
      _battleFeature.DisableSystems();
      _battleFeature.Dispose();
    }
  }
}