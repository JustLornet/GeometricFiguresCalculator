using GeometricFiguresCalculator.Figures.Circle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricFiguresCalculator.Tests
{
    public class TestsForCircle
    {
        [Test]
        public void StandartIntRadiusZeroDimension()
        {
            // arrange
            int radius = 10;

            // act
            var circle = new SimpleCircle(radius);
            var area = circle.CalculateArea(0);

            // assert
            Assert.That(area, Is.EqualTo(314));
        }

        [Test]
        public void StandartIntRadiusDefalutDimension()
        {
            // arrange
            int radius = 14;

            // act
            var circle = new SimpleCircle(radius);
            var area = circle.CalculateArea();

            // assert
            Assert.That(area, Is.EqualTo(615.75));
        }

        [Test]
        public void StandartFloatRadiusDefalutDimension()
        {
            // arrange
            var radius = 11.12;

            // act
            var circle = new SimpleCircle(radius);
            var area = circle.CalculateArea();

            // assert
            Assert.That(area, Is.EqualTo(388.47));
        }

        [Test]
        public void ZeroRadiusDefalutDimensionError()
        {
            // arrange
            var radius = 0;

            // act
            var exc = Assert.Throws<ArgumentException>(() => new SimpleCircle(radius));

            // assert
            Assert.That(exc.Message, Is.EqualTo("Радиус круга должен быть больше 0"));
        }

        [Test]
        public void MinusRadiusDefalutDimensionError()
        {
            // arrange
            var radius = -5;

            // act
            var exc = Assert.Throws<ArgumentException>(() => new SimpleCircle(radius));

            // assert
            Assert.That(exc.Message, Is.EqualTo("Радиус круга должен быть больше 0"));
        }
    }
}
