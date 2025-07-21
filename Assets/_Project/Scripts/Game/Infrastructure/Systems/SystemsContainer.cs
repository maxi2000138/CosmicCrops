using System;
using _Project.Scripts.Game.Features;
using _Project.Scripts.Infrastructure.Systems;
using VContainer.Unity;

namespace _Project.Scripts.Game.Infrastructure.Systems
{
  public class SystemsContainer : IInitializable ,ITickable, IFixedTickable, ILateTickable, IDisposable
  {
    private readonly Feature _feature;

    public SystemsContainer(Feature feature)
    {
      _feature = feature;
    }
    
    public void Initialize()
    {
      _feature.EnableSystems();
    }
    
    void ITickable.Tick()
    { 
      _feature.Update();
    }
    void IFixedTickable.FixedTick()
    { 
      _feature.FixedUpdate();
    }
    void ILateTickable.LateTick()
    {
        _feature.LateUpdate();
    }

    void IDisposable.Dispose()
    {
      _feature.DisableSystems();
      _feature.Dispose();
    }
  }
}