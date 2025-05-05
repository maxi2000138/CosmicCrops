using System;
using _Project.Scripts.Infrastructure.Factories.Systems;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Game.Infrastructure.Systems
{
  public class SystemsContainer : IInitializable ,ITickable, IFixedTickable, ILateTickable, IDisposable
  {
    private readonly ISystemFactory _systemFactory;
    private readonly IObjectResolver _objectResolver;

    private ISystem[] _systems = Array.Empty<ISystem>();

    public SystemsContainer(ISystemFactory systemFactory, IObjectResolver objectResolver)
    {
      _systemFactory = systemFactory;
      _objectResolver = objectResolver;
    }
    
    public void Initialize()
    {
      _systems = _systemFactory.CreateGameSystems();
      _systems.Foreach(_objectResolver.Inject);
      
      _systems.Foreach(Enable);
    }
    
    void ITickable.Tick()
    {
      for (int i = 0; i < _systems.Length; i++)
      {
        _systems[i].Update();
      }
    }
    void IFixedTickable.FixedTick()
    {
      for (int i = 0; i < _systems.Length; i++)
      {
        _systems[i].FixedUpdate();
      }
    }
    void ILateTickable.LateTick()
    {
      for (int i = 0; i < _systems.Length; i++)
      {
        _systems[i].LateUpdate();
      }
    }

    void IDisposable.Dispose()
    {
      _systems.Foreach(Disable);
      _systems = Array.Empty<ISystem>();
    }
    
    private void Enable(ISystem system)
    {
      system.EnableSystem();
    }

    private void Disable(ISystem system)
    {
      system.DisableSystem();
      system.Dispose();
    }
  }
}