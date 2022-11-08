namespace Game.Core.Interfaces
{
    public interface ICellService
    {
        void InitializeWord(string word);
        void Guess(char letter);
    }
}