using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Configurations/Game", fileName = nameof(GameConfiguration))]
    public class GameConfiguration : ScriptableObject
    {
        [field: TextArea(10, 30)]
        [field: SerializeField]
        public string Text
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public int MinLettersCount
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int TotalAttempts
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float DelayAfterSuccess
        {
            get;
            private set;
        }

        [SerializeField] private List<char> availableLetters;
        public IEnumerable<char> AvailableLetters => availableLetters;
    }
}