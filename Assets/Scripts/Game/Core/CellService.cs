using System.Collections.Generic;
using Abstractions.Interfaces;
using Game.Core.Interfaces;
using Game.UI.Cells;
using Game.UI.Cells.Interfaces;

namespace Game.Core
{
    public class CellService : ICellService
    {
        public CellService(IFactory<ICellView> viewFactory)
        {
            _viewFactory = viewFactory;
        }

        private readonly IFactory<ICellView> _viewFactory;

        private readonly IList<CellPresenter> _presenters = new List<CellPresenter>();
        
        private int _guessedCount = 0;

        public void InitializeWord(string word)
        {
            Clear();
            
            foreach (var letter in word)
            {
                var model = new CellModel(letter);
                
                var view = _viewFactory.Create();
                view.SetLetter(letter);
                
                var presenter = new CellPresenter(model, view);
                _presenters.Add(presenter);
            }
        }

        public void Guess(char letter)
        {
            var isRight = false;
            foreach (var presenter in _presenters)
            {
                if (presenter.Guess(letter))
                {
                    isRight = true;
                    _guessedCount++;
                }
            }

            GameSignals.TriedGuessLetter.Invoke(isRight);

            if (_guessedCount == _presenters.Count)
            {
                GameSignals.Success.Invoke();
            }
        }

        private void Clear()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }
            
            _presenters.Clear();
            _guessedCount = 0;
        }
    }
}