using System;
using _Project.Scripts.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.Progress
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ProgressService : IProgressService
    {
        public IData<int> LevelData { get; private set; }
        public IData<bool> HapticData { get; private set; }

        void IProgressService.Init()
        {
            LevelData = new LevelData();
            HapticData = new HapticData();
        }

        void IDisposable.Dispose()
        {
            LevelData?.Dispose();
        }
    }
}