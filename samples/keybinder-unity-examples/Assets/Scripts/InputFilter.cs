using System;
using System.Collections.Generic;
using UnityEngine;

namespace KeyBinder
{
    public class InputFilter
    {
        public KeyCode[] ValidKeys { get; private set; }
        public bool FilteringActive { get; private set; }

        public event Action<KeyCode> InvalidKeyReceived;

        public InputFilter()
        {
            ValidKeys = new KeyCode[0];
            FilteringActive = false;
        }

        public void SetKeys(KeyCode[] keys)
        {
            ValidKeys = keys;
            DetermineFilterActivity();
        }

        /// <summary>
        /// If you want to add a key for the input filering list.
        /// </summary>
        /// <param name="key">The key you want to add</param>
        public void AddKey(KeyCode key)
        {
            var current = new List<KeyCode>(ValidKeys);
            current.Add(key);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// If you want to add multiple keys for the input filering list.
        /// </summary>
        /// <param name="keys">Array with the keys you want to add</param>
        public void AddKeys(IEnumerable<KeyCode> keys)
        {
            var current = new List<KeyCode>(ValidKeys);
            current.AddRange(keys);
            ValidKeys = current.ToArray();
            DetermineFilterActivity();
        }

        /// <summary>
        /// Removes a certain KeyCode from the list of valid keys.
        /// </summary>
        /// <param name="key">A key you want to remove from the list of valid keys.</param>
        public void RemoveKey(KeyCode key)
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
        public void RemoveKeys(KeyCode[] keys)
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
        public void RemoveAll()
        {
            ValidKeys = null;
            DetermineFilterActivity();
        }

        // Checks the validity the key is valid
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

        void DetermineFilterActivity() =>
            FilteringActive = ValidKeys != null && ValidKeys.Length > 0;
    }
}