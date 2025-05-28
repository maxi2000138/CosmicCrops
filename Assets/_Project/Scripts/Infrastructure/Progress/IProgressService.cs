using System;

namespace _Project.Scripts.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        IData<int> LevelData { get; }
        IData<bool> HapticData { get; }
        void Init();
    }
}