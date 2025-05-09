﻿using R3;

namespace _Project.Scripts.Game.Features.Inventory
{
  public class InventoryModel
  {
    public ReactiveCommand<float> StartCollectingLoot { get; } = new ReactiveCommand<float>();
    public ReactiveCommand CancelCollectingLoot { get; } = new ReactiveCommand();
  }
}