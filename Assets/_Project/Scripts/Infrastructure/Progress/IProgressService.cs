using System;
using _Project.Scripts.Infrastructure.Progress.Data;

namespace _Project.Scripts.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        IData<int> LevelData { get; }
        IData<bool> HapticData { get; }
        IData<Inventory> InventoryData { get; }
        
        void Init();
    }
}