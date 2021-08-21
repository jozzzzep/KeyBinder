using System;
using System.Collections.Generic;
using UnityEngine;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/JosepeDev/KeyBinder
    /// <summary>
    /// The class that filters the input a <see cref="KeyDetector"/> receives
    /// </summary>
    public class InputFilter
    {
        /// <summary>
        /// The key's that are valid for the <see cref="KeyDetector"/> to receive, if empty - all keys are valid
        /// </summary>
        public List<KeyCode> ValidKeys
        {
            get => validKeysList;
            set
            {
                validKeysList = value;
                validKeys = validKeysList.ToArray();
                DetermineFilterActivity();
            }
        }

        /// <summary>
        /// Determines whether the key detector input filtering is active, 
        /// clear all valid keys to deactivate, add valid keys to activate filtering
        /// </summary>
        public bool FilteringActive { get; private set; }

        /// <summary>
        /// Raised when an invalid key has been detected
        /// </summary>
        public event Action<KeyCode> InvalidKeyReceived;

        private List<KeyCode> validKeysList;
        private KeyCode[] validKeys;

        internal InputFilter() =>
            ValidKeys = new List<KeyCode>();

        /// <summary>
        /// Derermines the validity of a given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>true if the key is valid</returns>
        public bool IsKeyValid(KeyCode key)
        {
            if (FilteringActive)
            {
                for (int i = 0; i < validKeys.Length; i++)
                    if (validKeys[i] == key)
                        return true;
                InvalidKeyReceived.SafeInvoke(key);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Called everytime the valid keys array changes
        /// </summary>
        private void DetermineFilterActivity() =>
            FilteringActive = validKeys != null && validKeys.Length > 0;
    }
}