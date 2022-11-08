using System;
using Game.UI.Letters.Interfaces;
using Unity.VisualScripting;

namespace Game.UI.Letters
{
    public class LetterPresenter : IInitializable, IDisposable
    {
        public LetterPresenter(ILetterModel model, ILetterView view)
        {
            _model = model;
            _view = view;
        }

        public event Action OnClick = () => { };

        private readonly ILetterModel _model;
        private readonly ILetterView _view;

        public void Initialize()
        {
            var letter = _model.GetLetter();

            _view.SetLetter(letter);
            _view.OnClicked += Click;
        }

        public void Dispose()
        {
            _view.OnClicked -= Click;
            _view.Dispose();
        }

        private void Click()
        {
            OnClick.Invoke();
        }
    }
}