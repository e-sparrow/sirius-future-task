using Game.UI.Letters.Interfaces;

namespace Game.UI.Letters
{
    public class LetterModel : ILetterModel
    {
        public LetterModel(char letter)
        {
            _letter = letter;
        }

        private readonly char _letter;
        
        public char GetLetter()
        {
            return _letter;
        }
    }
}