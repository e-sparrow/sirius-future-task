using System;
using Game.UI.Letters.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI.Letters
{
    public class LetterView : MonoBehaviour, ILetterView
    {
        public event Action OnClicked
        {
            add => button.onClick.AddListener(new UnityAction(value));
            remove => button.onClick.RemoveListener(new UnityAction(value));
        }

        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void SetLetter(char letter)
        {
            var value = letter.ToString();
            text.text = value;
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}