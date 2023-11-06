namespace TeamBuilder.Validations
{
    public class TextValidator
    {
        public bool TextValidation(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if ((text != null || text != string.Empty) && TextDigit(text) >= 1)
            {
                return true;
            }
            else
                return false;
        }


        private int TextDigit(string text)
        {
            int lenght = text.Length;
            if (lenght != 0)
                return lenght;
            else
                return 0;
        }
    }
}
