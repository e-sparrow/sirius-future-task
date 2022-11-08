using Game.UI.Cells.Interfaces;

namespace Game.UI.Cells
{
    public class CellPresenter
    {
        public CellPresenter(ICellModel model, ICellView view)
        {
            _model = model;
            _view = view;
        }

        private readonly ICellModel _model;
        private readonly ICellView _view;

        public bool Guess(char letter)
        {
            var isRight = _model.IsLetter(letter);
            if (isRight)
            {
                _view.Open();
            }

            return isRight;
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}