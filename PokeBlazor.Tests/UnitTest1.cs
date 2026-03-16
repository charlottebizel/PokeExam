using System;
using Xunit;

namespace PokeBlazor.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_String_IsNotNullOrEmpty()
        {
            string value = "Pikachu";
            Assert.False(string.IsNullOrEmpty(value)); // Vérifie que la chaîne n'est pas vide
        }

        [Fact]
        public void Test_Addition_Works()
        {
            int result = 2 + 3;
            Assert.Equal(5, result); // Vérifie que 2 + 3 = 5
        }

        [Fact]
        public void Test_String_Contains()
        {
            string name = "Charizard";
            Assert.Contains("Char", name); // Vérifie que "Char" est bien dans la chaîne
        }

        [Fact]
        public void Test_Exception_IsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => throw new ArgumentNullException()); // Vérifie qu'une exception est bien levée
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(0, false)]
        public void Test_IsPositive(int number, bool expected)
        {
            bool actual = number > 0;
            Assert.Equal(expected, actual); // Vérifie si le nombre est positif ou non
        }
    }
}