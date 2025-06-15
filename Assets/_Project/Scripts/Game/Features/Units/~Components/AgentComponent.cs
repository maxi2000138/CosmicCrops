using _Project.Scripts.Infrastructure.Systems.Components;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Game.Features.Units._Components
{
  public class AgentComponent : MonoComponent<AgentComponent>
  {
    [SerializeField] private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;
  }
}