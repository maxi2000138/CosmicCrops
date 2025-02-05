namespace _Project.Scripts.Infrastructure.Haptic.Engine
{
    public interface IHapticEngine
    {
        void Play(HapticType type);
        void IsEnable(bool isEnable);
    }
}