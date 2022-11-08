using System;
using Abstractions.Interfaces;

namespace Game.Core.Interfaces
{
    public interface IScoreService : IResettable
    {
        event Action<int> OnScoreChanged;

        void AddScore(int amount);

        int Score
        {
            get;
        }
    }
}