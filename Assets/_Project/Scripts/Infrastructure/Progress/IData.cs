using System;
using R3;

namespace _Project.Scripts.Infrastructure.Progress
{
    public interface IData<T> : IDisposable
    {
        ReactiveProperty<T> Data { get; }
    }
}