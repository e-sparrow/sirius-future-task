using System;

namespace Game.UI.Letters.Interfaces
{
    public interface ILetterView : IDisposable
    {
        event Action OnClicked;

        void SetLetter(char letter);
    }
}