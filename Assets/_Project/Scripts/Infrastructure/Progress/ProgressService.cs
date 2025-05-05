using System;
using CodeBase.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Progress
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