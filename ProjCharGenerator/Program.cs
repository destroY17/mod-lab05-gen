using System;

namespace generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string mainCatalogPath = "../../../../";

            Generate(new BigrammGenerator(), mainCatalogPath + "bigrammData.txt");
            Generate(new WordGenerator(), mainCatalogPath + "wordsData.txt");
            Generate(new PairWordGenerator(), mainCatalogPath + "pairWordsData.txt");
        }

        static void Generate(TextGenerator generator, string pathToSave = null, int textLength = 1000)
        {
            var text = generator.GenerateText(textLength);
            var probabilities = generator.GetProbabilities(text);

            foreach (var letter in text)
                Console.Write($"{letter} ");
            Console.WriteLine();

            foreach(var prob in probabilities)
                Console.WriteLine($"{prob.Key} - {prob.Value}");

            if (!(pathToSave is null))
                generator.SaveText(text, pathToSave);
        }
    }
}

