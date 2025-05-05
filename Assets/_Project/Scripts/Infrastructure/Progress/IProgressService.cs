using System;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        IData<int> LevelData { get; }
        void Init();
    }
}