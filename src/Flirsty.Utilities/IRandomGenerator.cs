using System;

namespace Flirsty.Utilities
{
    public interface IRandomGenerator
    {
        int NextInt(int min, int max);
    }

    public class RandomGenerator : IRandomGenerator
    {
        public int NextInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}