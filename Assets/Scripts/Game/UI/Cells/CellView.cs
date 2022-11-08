using Game.UI.Cells.Interfaces;
using TMPro;
using UnityEngine;

namespace Game.UI.Cells
{
    [AddComponentMenu(MenuName)]
    public class CellView : MonoBehaviour, ICellView
    {
        private const string MenuName = "Game/Core/CellView";
        
        [SerializeField] private Animator animator;
        [SerializeField] private string openTrigger;

        [SerializeField] private TMP_Text text;
        
        public void Open()
        {
            var trigger = Animator.StringToHash(openTrigger);
            animator.SetTrigger(trigger);
        }

        public void SetLetter(char letter)
        {
            text.text = letter.ToString();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}