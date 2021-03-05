using AONN.NN;
using System;
using System.Threading;

namespace ConsoleSimulation
{
    class Renderer
    {
        private const int _paddingLeft = 3;
        private const int _paddingTop = 3;
        private const int _width = 50;
        private const int _height = 30;

        private const int maxX = _paddingLeft + _width;
        private const int maxY = _paddingTop + _height;

        private const int _baseX = _paddingLeft + _width / 2;
        private const int _baseY = _paddingTop + _height / 2;

        private static readonly string _emptyLine = new string(' ', _width + 1);

        private readonly World _world;
        private readonly ConsoleOrganism _subject;
        private readonly NeuralNetworkCreationConfig _config;

        public Renderer(World world, ConsoleOrganism subject)
        {
            _world = world;
            _subject = subject;
            _config = (NeuralNetworkCreationConfig) subject.Config;
        }

        public void RenderAtFps(int fps)
        {
            var millisPerFrame = 1000 / fps;
            while (true)
            {
                RenderNow();
                Thread.Sleep(millisPerFrame);
            }
        }

        public void RenderNow()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_subject.CurrentTick);
            Console.WriteLine($"cs: {_config.CreationSeed}, s: {_config.Seed}");
            Console.SetCursorPosition(0, 0);

            ClearEntityRepresentations();
            DrawEntityRepresentations();

            Console.SetCursorPosition(0, 0);
        }

        private void ClearEntityRepresentations()
        {
            for (int i = 0; i < _height; i++)
            {
                Console.SetCursorPosition(_paddingLeft, _paddingTop + i);
                Console.Write(_emptyLine);
            }
            Console.SetCursorPosition(0, 0);
        }

        private void DrawEntityRepresentations()
        {
            foreach (var entity in _world.Entities)
            {
                var x = _baseX + entity.Position.X - _subject.Position.X;
                var y = _baseY - entity.Position.Y + _subject.Position.Y;
                
                if (x >= _paddingLeft 
                    && x <= maxX
                    && y >= _paddingTop
                    && y <= maxY)
                {
                    Console.SetCursorPosition(x, y);

                    Console.Write(entity.GetCurrentRepresentation());
                }

            }
            Console.SetCursorPosition(0, 0);
        }
    }
}
