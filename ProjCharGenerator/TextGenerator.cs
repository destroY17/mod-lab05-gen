using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace generator
{
    public abstract class TextGenerator
    {
        protected string[] alphabet;
        protected int[] weights;
        protected int[] upperBorders;

        private int _size;
        private int _totalSum;

        private Random _random = new Random();

        public TextGenerator()
        {
            GetAlphabet();
            GetWeights();

            _totalSum = weights.Sum();
            _size = alphabet.Length;

            upperBorders = new int[_size];
            GetUpperBorders();

            if (_size != upperBorders.Length)
                throw new Exception("There is no one-to-one" +
                "correspondence between alphabet and upperBorders");
        }

        protected abstract void GetAlphabet();
        protected abstract void GetWeights();
        protected abstract void GetUpperBorders();

        public string GenerateLetter()
        {
            int weight = _random.Next(0, _totalSum);
            int i;

            for (i = 0; i < _size; i++)
            {
                if (weight < upperBorders[i])
                    break;
            }
            return alphabet[i];
        }

        public string[] GenerateText(int length)
        {
            if (length < 0)
                throw new ArgumentException("length must be >= 0");

            if (length == 0)
                return null;

            var result = new string[length];
            for (int i = 0; i < length; i++)
                result[i] = GenerateLetter();

            return result;
        }

        public SortedDictionary<string, double> GetProbabilities(string[] text)
        {
            var probabilities = new SortedDictionary<string, double>();
            foreach (var letter in text)
            {
                probabilities.TryGetValue(letter, out double count);
                probabilities[letter] = count + 1;
            }

            foreach (var key in probabilities.Keys.ToList())
            {
                probabilities[key] /= text.Length;
            }

            return probabilities;
        }

        public void SaveText(string[] text, string filePath)
        {
            using StreamWriter sw = new StreamWriter(filePath);

            foreach (var letter in text)
                sw.Write($"{letter} ");
        }
    }
}
