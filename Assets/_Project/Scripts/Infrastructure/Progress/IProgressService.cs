using System;

namespace _Project.Scripts.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        IData<int> LevelData { get; }
        void Init();
    }
}