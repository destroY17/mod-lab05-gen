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
            var alphabet = "אבגדהוזחטיךכלםמןנסעףפץצקרשûü‎‏ÿ";

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

            Assert.IsTrue(probability["א"] > probability["ר"]);
            Assert.IsTrue(probability["מ"] > probability["ש"]);
            Assert.IsTrue(probability["ו"] > probability["כ"]);
        }

        [TestMethod]
        public void CheckWordGeneratorProbability()
        {
            var generator = new WordGenerator();
            var text = generator.GenerateText(10000);
            var probability = generator.GetProbabilities(text);

            Assert.IsTrue(probability["ט"] > probability["ךמעמנûי"]);
            Assert.IsTrue(probability["ג"] > probability["קונוח"]);
            Assert.IsTrue(probability["םו"] > probability["ךעמ"]);
        }

        [TestMethod]
        public void CheckPairWordGeneratorProbability()
        {
            var generator = new PairWordGenerator();
            var text = generator.GenerateText(10000);
            var probability = generator.GetProbabilities(text);

            Assert.IsTrue(probability["ט םו"] > probability["םוסלמענÿ םא"]);
            Assert.IsTrue(probability["ט ג"] > probability["קעמ ט"]);
            Assert.IsTrue(probability["ןמעמלף קעמ"] > probability["קעמ ט"]);
        }
    }
}
