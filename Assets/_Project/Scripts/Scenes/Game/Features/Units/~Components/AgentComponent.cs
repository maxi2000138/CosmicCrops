using _Project.Scripts.Game.UI.Pause.Interface;
using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Game.Features.Units._Components
{
  public class AgentComponent : MonoComponent<AgentComponent>, IPause
  {
    [SerializeField] private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;
    
    private float _speed;
    
    public void Pause(bool isPause)
    {
      if (isPause)
      {
        _speed = _agent.speed;
        _agent.speed = 0f;
      }
      else
      {
        _agent.speed = _speed;
      }
    }
  }
}