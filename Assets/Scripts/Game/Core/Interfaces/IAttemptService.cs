using System;
using Abstractions.Interfaces;

namespace Game.Core.Interfaces
{
    public interface IAttemptService : IResettable
    {
        event Action<int> OnCountChanged; 

        bool TrySpendAttempt();

        int Attempts
        {
            get;
        }
    }
}