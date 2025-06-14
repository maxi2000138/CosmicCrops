﻿using _Project.Scripts.Game.UI;
using _Project.Scripts.Game.UI.Canvas;
using _Project.Scripts.Game.UI.Screens;

namespace _Project.Scripts.Infrastructure.GUI
{
    public interface IGuiService
    {
        StaticCanvas StaticCanvas { get; }
        float ScaleFactor { get; }
        void Push(BaseScreen screen);
        void Pop();
        void Cleanup();
    }
}