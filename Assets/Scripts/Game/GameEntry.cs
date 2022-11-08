using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Interfaces;
using UnityEngine;

namespace Game
{
    public class GameEntry : MonoBehaviour, IEntry
    {
        [SerializeField] private List<ScriptableEntryBase> scriptableEntries;
        [SerializeField] private List<MonoEntryBase> monoEntries;

        private void Start()
        {
            if (IsEnabled)
            {
                Install();
            }
        }

        public void Install()
        {
            foreach (var entry in scriptableEntries)
            {
                if (entry.IsEnabled)
                {
                    entry.Install();
                }
            }

            foreach (var entry in monoEntries)
            {
                if (entry.IsEnabled)
                {
                    entry.Install();
                }
            }
        }

        [field: SerializeField]
        public bool IsEnabled
        {
            get;
            private set;
        }
    }
}
