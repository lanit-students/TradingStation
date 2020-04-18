using System;

namespace TestForSignUp
{
    internal class InputGenerator
    {
        internal string email;
        internal string date;
        internal string nameAndLastName;
        internal string password;
        internal InputGenerator(bool isCorrect)
        {
            if (isCorrect)
            {
                email = Guid.NewGuid().ToString() + "@gmail.com";
                nameAndLastName = "A";
                foreach (var i in Guid.NewGuid().ToString())
                {
                    if (char.IsLetter(i)) nameAndLastName += i;
                }
                password = "123";
                var rand = new Random();
                date = $"{rand.Next(10, 12)}{rand.Next(10, 29)}{rand.Next(1990, 2000)}";
            } else
            {
                email = "wrongEmail";
                nameAndLastName = "wrongName";
                date = "02022020";
                password = "123";
            }
        }
    }
}
