using AONN.NN;
using AONN.NN.Neurons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSimulation
{
    public class Processor
    {
        public delegate NeuralNetworkCreationConfig ConfigProvider();

        public static (Task[] tasks, ConsoleOrganism[] subjects) ProcessMultiple(
            int times,
            ConfigProvider configProvider,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronsInitializer,
            long maxTicks
            )
        {
            var tasks = new Task[times];
            var subjects = new ConsoleOrganism[times];

            for (int i = 0; i < times; i++)
            {
                var (task, subject) = CreateSubjectAndProcess(
                    configProvider(), 
                    inputNeuronInitializer, 
                    outputNeuronsInitializer, 
                    maxTicks
                    );

                tasks[i] = task;
                subjects[i] = subject;
            }

            return (tasks, subjects);
        }

        public static (Task task, ConsoleOrganism subject) CreateSubjectAndProcess(
            NeuralNetworkCreationConfig config,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronsInitializer,
            long maxTicks
            )
        {
            var subject = CreateSubjectWithWorld(config, inputNeuronInitializer, outputNeuronsInitializer);

            var task = Task.Factory.StartNew(() =>
                {
                    var tick = 0L;

                    while (tick < maxTicks)
                    {
                        subject.Tick(tick);
                        tick++;
                    }

                });

            return (task, subject);

        }

        public static (Task task, ConsoleOrganism subject) CreateSubjectAndProcessWithLimiter(
            NeuralNetworkCreationConfig config,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronsInitializer,
            long maxTicks,
            long ticksPerSecond
            )
        {
            var subject = CreateSubjectWithWorld(config, inputNeuronInitializer, outputNeuronsInitializer);

            var task = Task.Factory.StartNew(() =>
            {
                var tick = 0L;
                var lastTickMillis = 0L;

                var millisPerTick = 1000 / ticksPerSecond;

                var stopwatch = Stopwatch.StartNew();

                while (tick < maxTicks)
                {
                    if (stopwatch.ElapsedMilliseconds >= lastTickMillis + millisPerTick)
                    {
                        lastTickMillis = stopwatch.ElapsedMilliseconds;
                        subject.Tick(tick);
                        tick++;
                    }
                }

            });

            return (task, subject);
        }

        private static ConsoleOrganism CreateSubjectWithWorld(
            NeuralNetworkCreationConfig config,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronsInitializer
            )
        {
            var world = new World();

            var subject = ConsoleOrganismFactory.CreateConsoleOrgansim(
                world,
                config,
                inputNeuronInitializer,
                outputNeuronsInitializer
            );

            world.Entities.Add(subject);

            return subject;
        }
    }

}
