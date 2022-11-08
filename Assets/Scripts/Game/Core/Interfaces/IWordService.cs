using Abstractions.Interfaces;

namespace Game.Core.Interfaces
{
    public interface IWordService : IResettable
    {
        bool TryGetNextWord(out string word);
    }
}