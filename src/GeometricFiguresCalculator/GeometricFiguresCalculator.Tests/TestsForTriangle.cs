using GeometricFiguresCalculator.Figures.Circle;
using GeometricFiguresCalculator.Figures.Triangle;
using GeometricFiguresCalculator.Figures.Triangle.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricFiguresCalculator.Tests
{
    public class TestsForTriangle
    {
        [Test]
        public void StandartIntParametersZeroDimension()
        {
            // arrange
            Point A = new (1, 1);
            Point B = new (2, 2);
            Point C = new (0, 10);

            // act
            var triangle = new SimpleTriangle(A, B, C);
            var area = triangle.CalculateArea(0);
            
            // assert
            Assert.That(area, Is.EqualTo(5));
        }

        [Test]
        public void StandartIntParametersDefaultDimension()
        {
            // arrange
            Point A = new(8, 1);
            Point B = new(4, 2);
            Point C = new(1, 10);

            // act
            var triangle = new SimpleTriangle(A, B, C);
            var area = triangle.CalculateArea();

            // assert
            Assert.That(area, Is.EqualTo(14.50d));
        }

        [Test]
        public void NotRightangled()
        {
            // arrange
            Point A = new(8, 1);
            Point B = new(4, 2);
            Point C = new(1, 10);

            // act
            var triangle = new SimpleTriangle(A, B, C);
            var isRightangled = triangle.IsRightangled;

            // assert
            Assert.That(isRightangled, Is.False);
        }

        [Test]
        public void RightangledWithBVertex()
        {
            // arrange
            Point A = new(0, 0);
            Point B = new(0, 4);
            Point C = new(10, 4);

            // act
            var triangle = new SimpleTriangle(A, B, C);
            var isRightangled = triangle.IsRightangled;

            // assert
            Assert.That(isRightangled, Is.True);
        }

        [Test]
        public void StandartIntParametersDefaultDimensionRightangled()
        {
            // arrange
            Point A = new(0, 0);
            Point B = new(7, 0);
            Point C = new(7, 12);

            // act
            var triangle = new SimpleTriangle(A, B, C);
            var area = triangle.CalculateArea();
            var isRightangled = triangle.IsRightangled;
            
            // assert
            Assert.Multiple(() =>
            {
                Assert.That(area, Is.EqualTo(42));
                Assert.That(isRightangled, Is.True);
            });
        }

        [Test]
        public void ABVertexesZeroError()
        {
            // arrange
            Point A = new(0, 0);
            Point B = A;
            Point C = new(1,1);

            // act
            var exc = Assert.Throws<InvalidTriangleException>(() => new SimpleTriangle(A, B, C));

            // assert
            Assert.That(exc.Message, Is.EqualTo($"Две совпадащие вершины с координатасм {A}"));
        }

        [Test]
        public void BCVertexesZeroError()
        {
            // arrange
            Point A = new(1, 1);
            Point B = new Point(5,5);
            Point C = B;

            // act
            var exc = Assert.Throws<InvalidTriangleException>(() => new SimpleTriangle(A, B, C));

            // assert
            Assert.That(exc.Message, Is.EqualTo($"Две совпадащие вершины с координатасм {B}"));
        }
    }
}
