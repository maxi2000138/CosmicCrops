using System;
using R3;

namespace CodeBase.Infrastructure.Progress
{
    public interface IData<T> : IDisposable
    {
        ReadOnlyReactiveProperty<T> Data { get; }
    }
}