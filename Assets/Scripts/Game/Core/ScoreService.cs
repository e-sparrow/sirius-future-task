using System;
using Game.Core.Interfaces;

namespace Game.Core
{
    public class ScoreService : IScoreService
    {
        public ScoreService()
        {
            Score = 0;
        }
        
        public event Action<int> OnScoreChanged = _ => { };

        private int _score = 0;

        public void AddScore(int amount)
        {
            Score += amount;
        }
        
        public void Reset()
        {
            Score = 0;
        }
        
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged.Invoke(value);
            }
        }
    }
}