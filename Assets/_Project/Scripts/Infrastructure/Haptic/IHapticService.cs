using _Project.Scripts.Infrastructure.Haptic.Engine;

namespace _Project.Scripts.Infrastructure.Haptic
{
    public interface IHapticService
    {
        void Init();
        void Play(HapticType type);
        void IsEnable(bool isEnable);
    }
}