using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Interfaces;
using UnityEngine;

namespace Game.Core
{
    public class WordService : IWordService
    {
        public WordService(string source, int minLetters, IEnumerable<char> availableLetters)
        {
            var punctuation = source
                .Where(char.IsPunctuation)
                .Distinct()
                .ToArray();
            
            _initialWords = source
                .Split()
                .Select(value => value
                    .Trim(punctuation)
                    .ToLower())
                .Distinct()
                .Where(value => value.Length >= minLetters && value
                    .All(availableLetters.Contains))
                .ToList();

            _words = _initialWords;
        }

        private readonly List<string> _initialWords;

        private List<string> _words;

        public bool TryGetNextWord(out string word)
        {
            word = string.Empty;

            var result = _words.Count > 0;
            if (result)
            {
                var count = _initialWords.Count;
                var index = Random.Range(0, count);
            
                word = _words[index];
                _words.RemoveAt(index);
            }

            return result;
        }

        public void Reset()
        {
            _words = _initialWords;
        }
    }
}