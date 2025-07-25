﻿using System;

namespace _Project.Scripts.Infrastructure.AssetData
{
  public interface IAssetDownloadReporter : IProgress<float>
  {
    float Progress { get; }
    event Action ProgressUpdated;
    new void Report(float value);
    void Reset();
  }
}