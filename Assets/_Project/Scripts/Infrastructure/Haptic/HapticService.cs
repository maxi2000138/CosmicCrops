﻿using _Project.Scripts.Infrastructure.Haptic.Engine;
using JetBrains.Annotations;

namespace _Project.Scripts.Infrastructure.Haptic
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class HapticService : IHapticService
    {
        private IHapticEngine _engine;

        void IHapticService.Init() => _engine = new HapticEngine();
        void IHapticService.Play(HapticType type) => _engine.Play(type);
        void IHapticService.IsEnable(bool isEnable) => _engine.IsEnable(isEnable);
    }
}