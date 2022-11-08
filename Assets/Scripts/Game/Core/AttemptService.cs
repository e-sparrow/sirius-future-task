using System;
using Game.Core.Interfaces;

namespace Game.Core
{
    public class AttemptService : IAttemptService
    {
        public AttemptService(int attempts)
        {
            _initialAttempts = attempts;
            Attempts = attempts;
        }

        public event Action<int> OnCountChanged = _ => { };
        
        private readonly int _initialAttempts;
        
        private int _attempts;

        public bool TrySpendAttempt()
        {
            Attempts--;

            var success = Attempts > 0;
            return success;
        }

        public void Reset()
        {
            Attempts = _initialAttempts;
        }

        public int Attempts
        {
            get => _attempts;
            private set
            {
                _attempts = value;
                OnCountChanged.Invoke(value);
            }
        }
    }
}