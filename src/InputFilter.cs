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
        public KeyCode[] ValidKeys { get; private set; }

        /// <summary>
        /// Determines whether the key detector input filtering is active, 
        /// clear all valid keys to deactivate, add valid keys to activate filtering
        /// </summary>
        public bool FilteringActive { get; private set; }

        /// <summary>
        /// Raised when an invalid key has been detected
        /// </summary>
        public event Action<KeyCode> InvalidKeyReceived;

        internal InputFilter()
        {
            ValidKeys = new KeyCode[0];
            FilteringActive = false;
        }

        /// <summary>
        /// Set the valid keys array
        /// </summary>
        public void ValidKeysSet(KeyCode[] keys)
        {
            ValidKeys = keys;
            DetermineFilterActivity();
        }

        /// <summary>
        /// If you want to add a key for the input filering list
        /// </summary>
        /// <param name="key">The key you want to add</param>
        public void ValidKeysAdd(KeyCode key)
        {
            var current = new List<KeyCode>(ValidKeys);
            current.Add(key);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// If you want to add multiple keys for the input filering list
        /// </summary>
        /// <param name="keys">Array with the keys you want to add</param>
        public void ValidKeysAdd(IEnumerable<KeyCode> keys)
        {
            var current = new List<KeyCode>(ValidKeys);
            current.AddRange(keys);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// Removes a certain KeyCode from the list of valid keys if it exists there
        /// </summary>
        /// <param name="key">A key you want to remove from the list of valid keys.</param>
        public void ValidKeysRemove(KeyCode key)
        {
            var current = new List<KeyCode>(ValidKeys);
            if (current.Count > 0)
                if (current.Contains(key))
                    current.Remove(key);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// Removes a bunch of KeyCodes from the valid keys if they are contained 
        /// </summary>
        /// <param name="key">An array of keys you want to remove from the list of valid keys.</param>
        public void ValidKeysRemove(KeyCode[] keys)
        {
            var current = new List<KeyCode>(ValidKeys);
            if (current.Count > 0)
                for (int i = 0; i < keys.Length; i++)
                    if (current.Contains(keys[i]))
                        current.Remove(keys[i]);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// Clears the list of valid keys and turns of input filtering
        /// </summary>
        public void ValidKeysRemoveAll()
        {
            ValidKeys = null;
            DetermineFilterActivity();
        }

        /// <summary>
        /// Derermines the validity of a given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>true if the key is valid</returns>
        public bool IsKeyValid(KeyCode key)
        {
            if (FilteringActive)
            {
                for (int i = 0; i < ValidKeys.Length; i++)
                    if (ValidKeys[i] == key)
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
            FilteringActive = ValidKeys != null && ValidKeys.Length > 0;
    }
}