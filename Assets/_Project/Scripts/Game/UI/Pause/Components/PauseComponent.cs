using System;
using _Project.Scripts.Game.Features.Pause.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;

namespace _Project.Scripts.Game.Features.Pause.Components
{
  public sealed class PauseComponent : MonoComponent<PauseComponent>
  {
    private IPause[] _pauses = Array.Empty<IPause>();

    public override void OnComponentCreate()
    {
      base.OnComponentCreate();
      
      _pauses = GetComponentsInChildren<IPause>();
    }

    public void Pause(bool isPause)
    {
      for (int i = 0; i < _pauses.Length; i++)
      {
        _pauses[i].Pause(isPause);
      }
    }
  }
}