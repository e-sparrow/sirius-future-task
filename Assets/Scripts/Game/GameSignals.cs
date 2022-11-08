using Utils.Signals;
using Utils.Signals.Interfaces;

namespace Game
{
    public static class GameSignals
    {
        public static readonly ISignal Start = new Signal();
        public static readonly ISignal GameOver = new Signal();
        public static readonly ISignal Success = new Signal();

        public static readonly ISignal<string> InitializeWord = new Signal<string>();
        public static readonly ISignal<char> GuessLetter = new Signal<char>();

        public static readonly ISignal<bool> TriedGuessLetter = new Signal<bool>();

        public static readonly ISignal<int> ScoreChanged = new Signal<int>();
        public static readonly ISignal<int> AttemptsCountChanged = new Signal<int>();
    }
}