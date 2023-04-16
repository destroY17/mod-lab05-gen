using Microsoft.VisualStudio.TestTools.UnitTesting;
using generator;
using System;

namespace Tests
{
    [TestClass]
    public class TextGeneratorTest
    {
        [TestMethod]
        public void CheckGenerateLetter()
        {
            var alphabet = "абвгдежзийклмнопрстуфхцчшщыьэюя";

            var generator = new BigrammGenerator();
            var letter = generator.GenerateLetter();

            Assert.AreEqual(letter.Length, 1);
            
            for (int i = 0; i < 1000; i++)
            {
                letter = generator.GenerateLetter();
                Assert.IsTrue(alphabet.Contains(letter.Substring(0, 1)));
            }
        }

        [TestMethod]
        public void CheckGenerateText()
        {
            var text = new BigrammGenerator().GenerateText(0);
            Assert.IsNull(text);

            text = new WordGenerator().GenerateText(100);
            Assert.AreEqual(text.Length, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncorrectGenerateTextParameters() =>
            new PairWordGenerator().GenerateText(-10);

        [TestMethod]
        public void CheckBigrammGeneratorProbability()
        {
            var generator = new BigrammGenerator();
            var text = generator.GenerateText(10000);
            var probability = generator.GetProbabilities(text);

            Assert.IsTrue(probability["а"] > probability["ш"]);
            Assert.IsTrue(probability["о"] > probability["щ"]);
            Assert.IsTrue(probability["е"] > probability["л"]);
        }

        [TestMethod]
        public void CheckWordGeneratorProbability()
        {
            var generator = new WordGenerator();
            var text = generator.GenerateText(10000);
            var probability = generator.GetProbabilities(text);

            Assert.IsTrue(probability["и"] > probability["который"]);
            Assert.IsTrue(probability["в"] > probability["через"]);
            Assert.IsTrue(probability["не"] > probability["кто"]);
        }

        [TestMethod]
        public void CheckPairWordGeneratorProbability()
        {
            var generator = new PairWordGenerator();
            var text = generator.GenerateText(10000);
            var probability = generator.GetProbabilities(text);

            Assert.IsTrue(probability["и не"] > probability["несмотря на"]);
            Assert.IsTrue(probability["и в"] > probability["что и"]);
            Assert.IsTrue(probability["потому что"] > probability["таким образом"]);
        }
    }
}
