using System.Collections.Generic;
using Abstractions.Interfaces;
using Game.Core.Interfaces;
using Game.UI.Letters;
using Game.UI.Letters.Interfaces;

namespace Game.Core
{
    public class LetterService : ILetterService
    {
        public LetterService(IFactory<ILetterView> viewFactory)
        {
            _viewFactory = viewFactory;
        }
        
        private readonly IFactory<ILetterView> _viewFactory;

        private readonly IList<LetterPresenter> _presenters = new List<LetterPresenter>();

        public void InitializeLetters(IEnumerable<char> letters)
        {
            Clear();
            
            foreach (var letter in letters)
            {
                var model = new LetterModel(letter);
                var view = _viewFactory.Create();

                var presenter = new LetterPresenter(model, view);
                presenter.Initialize();
                presenter.OnClick += Click;
                
                _presenters.Add(presenter);

                void Click()
                {
                    presenter.OnClick -= Click;
                    
                    _presenters.Remove(presenter);
                    presenter.Dispose();
                    
                    GameSignals.GuessLetter.Invoke(letter);
                }
            }
        }

        private void Clear()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }
            
            _presenters.Clear();
        }
    }
}