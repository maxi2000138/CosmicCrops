namespace _Project.Scripts.Infrastructure.Haptic.Engine
{
    public interface IHapticAdapter
    {
        void Play(HapticType type);
        bool IsSupported();
    }
}