using System.Collections.Generic;

namespace Game.Core.Interfaces
{
    public interface ILetterService
    {
        void InitializeLetters(IEnumerable<char> letters);
    }
}