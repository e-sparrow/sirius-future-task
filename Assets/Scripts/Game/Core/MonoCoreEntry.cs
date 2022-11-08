using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using Game.UI.Cells;
using Game.UI.Letters;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core
{
    public class MonoCoreEntry : MonoEntryBase
    {
        [Header("General")]
        [SerializeField] private GameConfiguration gameConfiguration;
        
        [Header("Cells")]
        [SerializeField] private CellView cellPrefab;
        [SerializeField] private Transform cellRoot;

        [Header("Letters")] 
        [SerializeField] private LetterView letterPrefab;
        [SerializeField] private Transform letterRoot;

        [Header("UI")] 
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text attemptsText;
        
        protected override void InstallInternal()
        {
            InstallCells();
            InstallLetters();
            InstallUI();
            InstallServices();
            
            GameSignals.Start.Invoke();
        }

        private void InstallServices()
        {
            var attemptService = new AttemptService(gameConfiguration.TotalAttempts);
            attemptService.OnCountChanged += ChangeAttemptsCount;
            GameSignals.TriedGuessLetter.OnInvoke += TryGuessLetter;

            var scoreService = new ScoreService();
            scoreService.OnScoreChanged += ChangeScore;

            var wordService = new WordService(gameConfiguration.Text, gameConfiguration.MinLettersCount, gameConfiguration.AvailableLetters);
            
            GameSignals.Success.OnInvoke += Success;
            GameSignals.GameOver.OnInvoke += GameOver;
            GameSignals.Start.OnInvoke += Start;

            void TryGuessLetter(bool success)
            {
                if (!success)
                {
                    var haveAttempts = attemptService.TrySpendAttempt();
                    if (!haveAttempts)
                    {
                        GameSignals.GameOver.Invoke();
                    }
                }
            }

            void ChangeAttemptsCount(int count)
            {
                GameSignals.AttemptsCountChanged.Invoke(count);
            }

            void ChangeScore(int score)
            {
                GameSignals.ScoreChanged.Invoke(score);
            }

            void Success()
            {
                StopAllCoroutines();
                StartCoroutine(SuccessCoroutine());

                IEnumerator SuccessCoroutine()
                {
                    var delay = gameConfiguration.DelayAfterSuccess;
                    yield return new WaitForSeconds(delay);
                    
                    var score = attemptService.Attempts;
                    scoreService.AddScore(score);
                
                    attemptService.Reset();
                
                    Start();
                }
            }

            void GameOver()
            {
                scoreService.Reset();
                attemptService.Reset();
                
                Start();
            }

            void Start()
            {
                var hasWord = wordService.TryGetNextWord(out var word);
                if (hasWord)
                {
                    GameSignals.InitializeWord.Invoke(word);
                }
                else
                {
                    wordService.Reset();
                    
                    scoreService.Reset();
                    attemptService.Reset();

                    var hasAnyWord = wordService.TryGetNextWord(out var nextWord);
                    if (!hasAnyWord)
                    {
                        throw new ArgumentException("Text doesn't contain any fit word! Try do decrease min letters count");
                    }

                    GameSignals.InitializeWord.Invoke(nextWord);
                }
            }
        }

        private void InstallCells()
        {
            var viewFactory = new PrefabFactory<CellView>(cellPrefab, cellRoot);
            var service = new CellService(viewFactory);

            GameSignals.InitializeWord.OnInvoke += service.InitializeWord;
            GameSignals.GuessLetter.OnInvoke += service.Guess;
        }

        private void InstallLetters()
        {
            var viewFactory = new PrefabFactory<LetterView>(letterPrefab, letterRoot);
            var service = new LetterService(viewFactory);

            GameSignals.InitializeWord.OnInvoke += InitializeLetters;

            void InitializeLetters(string word)
            {
                service.InitializeLetters(gameConfiguration.AvailableLetters);
            }
        }

        private void InstallUI()
        {
            SetAttemptsCount(gameConfiguration.TotalAttempts);
            SetScore(0);
            
            GameSignals.AttemptsCountChanged.OnInvoke += SetAttemptsCount;
            GameSignals.ScoreChanged.OnInvoke += SetScore;

            void SetAttemptsCount(int count)
            {
                attemptsText.text = $"Attempts: <color=#FF7D7D>{count}</color>";
            }

            void SetScore(int score)
            {
                scoreText.text = $"Score: <color=#5AC8C8>{score}</color>";
            }
        }
    }
}