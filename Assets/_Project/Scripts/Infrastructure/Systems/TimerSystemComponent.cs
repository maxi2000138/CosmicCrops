using _Project.Scripts.Infrastructure.Systems.Components;
using _Project.Scripts.Infrastructure.Time;

namespace _Project.Scripts.Infrastructure.Systems
{
  public abstract class TimerSystemComponent<T> : SystemComponent<T> where T : IComponent
  {
    private readonly ITimeService _time;
    private readonly float _executeIntervalSeconds;
        
    private float _timeToExecute;

    public TimerSystemComponent(float executeIntervalSeconds, ITimeService time)
    {
      _executeIntervalSeconds = executeIntervalSeconds;
      _time = time;
    }

    protected abstract void OnTimerUpdate();

    protected override void OnUpdate()
    {
      _timeToExecute -= _time.DeltaTime;
      if (_timeToExecute > 0)
        return;
      
      _timeToExecute = _executeIntervalSeconds - _timeToExecute;
      OnTimerUpdate();
    }
  }
}