namespace ConsoleSimulation
{

    public class Vector2d
    {

        public Vector2d(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public void Add(Vector2d v2)
        {
            Add(v2.X, v2.Y);
        }

        public void Add(double x, double y)
        {
            X += x;
            Y += y;
        }

        public void Set(Vector2d v2)
        {
            Set(v2.X, v2.Y);
        }

        public void Set(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Reset()
        {
            X = 0;
            Y = 0;
        }

        public void RotateClockwise()
        {
            var prevX = X;

            X = Y;
            Y = -prevX;
        }

        public void RotateCounterClockwise()
        {
            var prevY = Y;

            Y = X;
            X = -prevY;
        }

        public void Reverse()
        {
            X = -X;
            Y = -Y;
        }

        public Vector2i ToInt()
        {
            return new Vector2i((int)X, (int)Y);
        }
    }

    public class Vector2i
    {
        public Vector2i(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public void Add(Vector2i v2)
        {
            Add(v2.X, v2.Y);
        }

        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Set(Vector2i v2)
        {
            Set(v2.X, v2.Y);
        }

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Reset()
        {
            X = 0;
            Y = 0;
        }

        public void RotateClockwise()
        {
            var prevX = X;

            X = Y;
            Y = -prevX;
        }

        public void RotateCounterClockwise()
        {
            var prevY = Y;

            Y = X;
            X = -prevY;
        }

        public void Reverse()
        {
            X = -X;
            Y = -Y;
        }

        public Vector2d ToDouble()
        {
            return new Vector2d(X, Y);
        }
    }
}
