﻿using GeometricFiguresCalculator.Factory;
using GeometricFiguresCalculator.Service;

namespace GeometricFiguresCalculator.Figures.Circle
{
    [Figure("simple-circle")]
    public sealed class SimpleCircle : CircleBase
    {
        public SimpleCircle(double radius) : base(radius)
        {
            SendConsoleMessage.DefaultNotification(Name);
        }
    }
}
