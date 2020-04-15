using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace TestForSignUp
{
    class InputGenerator
    {
        internal string emailString;
        internal string dateString;
        internal string commonString;

        internal InputGenerator()
        {
            emailString = Guid.NewGuid().ToString() + "@gmail.com";
            commonString = "A";
            foreach (var i in Guid.NewGuid().ToString()) if (char.IsLetter(i)) commonString += i;
            var rand = new Random();
            dateString = $"{rand.Next(10, 29)}{rand.Next(10, 12)}{rand.Next(1990, 2000)}";
        }
    }
}
