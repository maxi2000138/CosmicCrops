namespace _Project.Scripts.Infrastructure.Time
{
  public class TimeService : ITimeService
  {
    public float DeltaTime => UnityEngine.Time.deltaTime;
  }
}