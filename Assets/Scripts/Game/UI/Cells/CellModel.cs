using Game.UI.Cells.Interfaces;

namespace Game.UI.Cells
{
    public class CellModel : ICellModel
    {
        public CellModel(char letter)
        {
            _letter = letter;
        }

        private readonly char _letter;
        
        public bool IsLetter(char letter)
        {
            var result = _letter == letter;
            return result;
        }
    }
}