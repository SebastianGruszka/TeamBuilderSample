using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamBuilder.Validations
{
    public class PhoneNumberValidator
    {
        /// <summary>
        /// Phones the number is valid.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>A bool.</returns>
        public bool PhoneNumberIsValid(string number)
        {
            if (PhoneNumberDigitlenght(number) == 9)
                return true;
            if ((PhoneNumberDigitlenght(number) == 11) || (PhoneNumberDigitlenght(number) == 14))
                return true;
            else
                return false;

        }

        /// <summary>
        /// Phones the number digitlenght.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>An int.</returns>
        public int PhoneNumberDigitlenght(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            int lenght = text.Length;
            if (lenght != 0)
                return lenght;
            else
                return 0;
        }
    }
}
