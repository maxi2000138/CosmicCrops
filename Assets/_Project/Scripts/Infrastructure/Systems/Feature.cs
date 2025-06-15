using System.Collections.Generic;
using _Project.Scripts.Utils.Extensions;
using VContainer;

namespace _Project.Scripts.Infrastructure.Systems
{
  public abstract class Feature : ISystem
  {
    private readonly List<ISystem> _systems = new List<ISystem>();
    private readonly IObjectResolver _objectResolver;

    protected Feature(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
    }

    protected void Add(ISystem system)
    {
      _objectResolver.Inject(system);
      _systems.Add(system);
    }
    
    public void EnableSystems()
    {
      _systems.Foreach(x => x.EnableSystems());
    }
    public void Update()
    {
      _systems.Foreach(x => x.Update());
    }
    
    public void FixedUpdate()
    {
      _systems.Foreach(x => x.FixedUpdate());
    }
    
    public void LateUpdate()
    {
      _systems.Foreach(x => x.LateUpdate());
    }
    
    public void DisableSystems()
    {
      _systems.Foreach(x => x.DisableSystems());
    }

    public void Dispose()
    {
      _systems.Foreach(x => x.Dispose());
    }
  }
}