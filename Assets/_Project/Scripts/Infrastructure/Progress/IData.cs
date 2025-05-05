using System;
using R3;

namespace _Project.Scripts.Infrastructure.Progress
{
    public interface IData<T> : IDisposable
    {
        ReadOnlyReactiveProperty<T> Data { get; }
    }
}