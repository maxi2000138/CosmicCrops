using _Project.Scripts.Infrastructure.Logger;

namespace _Project.Scripts.Infrastructure.Haptic.Engine
{
    public sealed class HapticAdapterDummy : IHapticAdapter
    {
        void IHapticAdapter.Play(HapticType type)
        {
#if UNITY_EDITOR
            DebugLogger.Log($"Impact haptic {type}", LogsType.Haptic);
#endif
        }

        bool IHapticAdapter.IsSupported()
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }
}