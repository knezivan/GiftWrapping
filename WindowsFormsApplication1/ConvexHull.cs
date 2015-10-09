using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class ConvexHull
    {
        public static List<Point> CH2(List<Point> points)
        {
            List<Point> vertices = new List<Point>();

            if (points.Count == 0)
                return null;
            else if (points.Count == 1)
            {
                // Ako je samo jedna tačka, vrati je
                vertices.Add(points[0]);
                return vertices;
            }


            Point leftMost = CH2Init(points);
            vertices.Add(leftMost);

            Point prev = leftMost;
            Point? next;
            double rot = 0;
            do
            {
                next = CH2Step(prev, points, ref rot);

                // Ako nije prvo teme (krajnje levo) ukloni ga
                if (prev != leftMost)
                    points.Remove(prev);

                // Ako nije poslednje teme, sačuvaj ga
                if (next.HasValue)
                {
                    vertices.Add(next.Value);
                    prev = next.Value;
                }

            } while (points.Count > 0 && next.HasValue && next.Value != leftMost);
            points.Remove(leftMost);

            return vertices;

        }

        private static Point CH2Init(List<Point> points)
        {
            // Inicijalizacija - Pronalaženje krajnje leve tačke
            Point leftMost = points[0];
            int leftX = leftMost.X;

            foreach (Point p in points)
            {
                if (p.X < leftX)
                {
                    leftMost = p;
                    leftX = p.X;
                }
            }
            return leftMost;
        }

        private static Point? CH2Step(Point currentPoint, List<Point> points, ref double rot)
        {
            double angle, angleRel, smallestAngle = 2 * Math.PI, smallestAngleRel = 4 * Math.PI;
            Point? chosen = null;
            int xDiff, yDiff;

            foreach (Point candidate in points)
            {
                if (candidate == currentPoint)
                    continue;

                xDiff = candidate.X - currentPoint.X;
                yDiff = -(candidate.Y - currentPoint.Y); //Y-osa počinje od vrha
                angle = ComputeAngle(new Point(xDiff, yDiff));

                // angleRel je ugao između linije i zarotirane y ose
                // y osa ima pravac poslednje izračunate pomoćne linije
                // koji je dat u promenljivoj rot.

                angleRel = 2 * Math.PI - (rot - angle);

                if (angleRel >= 2 * Math.PI)
                    angleRel -= 2 * Math.PI;
                if (angleRel < smallestAngleRel)
                {
                    smallestAngleRel = angleRel;
                    smallestAngle = angle;
                    chosen = candidate;
                }

            }

            // Čuvanje najmanjeg ugla kao rotaciju y ose za računanje
            // sledeće pomoćne linije

            rot = smallestAngle;

            return chosen;
        }


        //Računanje ugla rotacije y ose do sledeće tačke omotača
        private static double ComputeAngle(Point p)
        {
            if (p.X > 0 && p.Y > 0)
                return Math.Atan((double)p.X / p.Y);
            else if (p.X > 0 && p.Y == 0)
                return (Math.PI / 2);
            else if (p.X > 0 && p.Y < 0)
                return (Math.PI + Math.Atan((double)p.X / p.Y));
            else if (p.X == 0 && p.Y >= 0)
                return 0;
            else if (p.X == 0 && p.Y < 0)
                return Math.PI;
            else if (p.X < 0 && p.Y > 0)
                return (2 * Math.PI + Math.Atan((double)p.X / p.Y));
            else if (p.X < 0 && p.Y == 0)
                return (3 * Math.PI / 2);
            else if (p.X < 0 && p.Y < 0)
                return (Math.PI + Math.Atan((double)p.X / p.Y));
            else
                return 0;
        }
    }
}
