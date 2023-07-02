using GeometricFiguresCalculator.Service;
using System.Drawing;

namespace GeometricFiguresCalculator.ValueObjects
{
    public struct Angle
    {
        private readonly Point _mainApex;
        private readonly Point _pointA;
        private readonly Point _pointB;

        private double _angle = -1;
        private bool? _isRightangled = null;

        private Point _vectorA = new();
        private Point _vectorB = new();
        private int _scalarProduct = -1;

        /// <summary>
        /// Угол, образованный тремя точками
        /// Вычисление и проверка угла происходят на этапе создания объекта
        /// </summary>
        /// <param name="mainApex">Вершина, для которой определяется угол</param>
        /// <param name="pointA">Точка, к которой идет вектор из вершины угла</param>
        /// <param name="pointB">Точка, к которой идет вектор из вершины угла</param>
        public Angle(Point mainApex, Point pointA, Point pointB)
        {
            _mainApex = mainApex;
            _pointA = pointA;
            _pointB = pointB;

            this.CalculateBaseParameters();
        }

        public double Value
        {
            get
            {
                if(_angle >= 0) return _angle;

                if(_isRightangled is not null && (bool)_isRightangled)
                {
                    _angle = 90;

                    return _angle;
                }

                var angle = CalculateAngle();

                // проверки корректности угла
                if (_angle < 0 || _angle > 360)
                    throw new InvalidOperationException($"Угол не может иметь значение {_angle}");

                _angle = angle;

                return _angle;
            }
        }

        public bool IsRightangled
        {
            get
            {
                if (_isRightangled is not null) return (bool)_isRightangled;

                // если скалярное произведение равно нулю, то угол будет 90 C
                if (_scalarProduct == 0) _isRightangled = true;
                // если длина одного из векторов равна 0, то угол будет 90 C
                else if (_vectorA.X == 0 && _vectorA.Y == 0) _isRightangled = true;
                else _isRightangled = _vectorB.X == 0 && _vectorB.Y == 0;

                return (bool)_isRightangled;
            }
        }

        public static explicit operator double(Angle angle) => angle.Value;

        /// <summary>
        /// Расчет базовых параметров, которые в любом случае понадобятся для дальнейших вычислений
        /// </summary>
        private void CalculateBaseParameters()
        {
            // поиск векторов
            _vectorA = _mainApex.Subtract(_pointA);
            _vectorB = _mainApex.Subtract(_pointB);

            // скалярное произведение векторов
            _scalarProduct = _vectorA.X * _vectorB.X + _vectorA.Y * _vectorB.Y;
        }

        private double CalculateAngle()
        {
            // длины векторов
            double vectorALength = Math.Sqrt(Math.Pow(_vectorA.X, 2) + Math.Pow(_vectorA.Y, 2));
            double vectorBLength = Math.Sqrt(Math.Pow(_vectorB.X, 2) + Math.Pow(_vectorB.Y, 2));

            // вычисляем косинус угла
            var angleCos = _scalarProduct / (vectorALength * vectorBLength);

            return Math.Acos(angleCos);
        }
    }
}
