using GeometricFiguresCalculator.Figures.Circle;
using GeometricFiguresCalculator.Figures.Triangle.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricFiguresCalculator.Tests
{
    public class TestsForAreaCalculator
    {
        [Test]
        public void StandartCircle()
        {
            // act
            var standartCircleArea = AreaCalculator.GetCircleArea(14);

            // assert
            Assert.That(standartCircleArea, Is.EqualTo(615.75));
        }

        [Test]
        public void ErrorCircle()
        {
            // act
            var exc = Assert.Throws<ArgumentException>(() => AreaCalculator.GetCircleArea(0));

            // assert
            Assert.That(exc.Message, Is.EqualTo("Радиус круга должен быть больше 0"));
        }

        [Test]
        public void StandartTriangle()
        {
            // arrange
            var A = new Point(1, 1);
            var B = new Point(2, 2);
            var C = new Point(0, 10);

            // act
            var standartTriangleArea = AreaCalculator.GetTriangleArea(A, B, C, out bool isRighttangled);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(standartTriangleArea, Is.EqualTo(5));
                Assert.That(isRighttangled, Is.False);
            });
        }

        [Test]
        public void RightangledTriangle()
        {
            // arrange
            var A = new Point(0, 0);
            var B = new Point(0, 2);
            var C = new Point(10, 2);

            // act
            var standartTriangleArea = AreaCalculator.GetTriangleArea(A, B, C, out bool isRighttangled);

            // assert
                Assert.That(isRighttangled, Is.True);
        }

        [Test]
        public void ErrorTriangle()
        {
            // arrange
            var A = new Point(0, 0);
            var B = new Point(0, 0);
            var C = new Point(0, 10);

            // act
            var exc = Assert.Throws<InvalidTriangleException>(() => AreaCalculator.GetTriangleArea(A, B, C, out bool isRighttangled));

            // assert
                Assert.That(exc, Is.TypeOf(typeof(InvalidTriangleException)));
        }

        [Test]
        public void StandartCircleViaName()
        {
            // arrange
            var calculator = new AreaCalculator();

            // act
            var circleArea = calculator.GetFigureAreaViaName("simple-circle", 0, 10);

            // arrange
            Assert.That(circleArea, Is.EqualTo(314));
        }
    }
}
