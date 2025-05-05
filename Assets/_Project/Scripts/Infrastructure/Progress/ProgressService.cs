using System;
using _Project.Scripts.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.Progress
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ProgressService : IProgressService
    {
        public IData<int> LevelData { get; private set; }

        void IProgressService.Init()
        {
            LevelData = new LevelData();
        }

        void IDisposable.Dispose()
        {
            LevelData?.Dispose();
        }
    }
}