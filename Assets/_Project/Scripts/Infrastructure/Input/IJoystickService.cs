using _Project.Scripts.Infrastructure.Time;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Input
{
    public interface IJoystickService
    {
        Vector2 GetAxis();
        float GetDeadZone();
        void Init(ITimeService time);
        void Enable(bool isEnable);
        void Execute();
    }
}