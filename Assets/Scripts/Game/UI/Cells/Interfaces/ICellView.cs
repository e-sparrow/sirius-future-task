using System;

namespace Game.UI.Cells.Interfaces
{
    public interface ICellView : IDisposable
    {
        void Open();
        
        void SetLetter(char letter);
    }
}