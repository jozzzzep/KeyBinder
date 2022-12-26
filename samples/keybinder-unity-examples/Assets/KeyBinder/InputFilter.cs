using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/jozzzzep/KeyBinder
    /// <summary>
    /// The class that filters the input a <see cref="KeyDetector"/> receives
    /// </summary>
    public class InputFilter
    {
        public enum Method
        {
            /// <summary>
            /// The key will be valid only if it is inside the keys list
            /// </summary>
            Whitelist,
            /// <summary>
            /// If the key is inside the keys list it'll be invalid
            /// </summary>
            Blacklist
        }

        /// <summary>
        /// The key's that are valid/invalid for the <see cref="KeyDetector"/> to receive, if empty - all keys are valid
        /// </summary>
        public KeyCode[] Keys => keysList;

        /// <summary>
        /// Determines whether the key detector input filtering is active, 
        /// clear all valid keys to deactivate, add valid keys to activate filtering
        /// </summary>
        public bool FilteringActive { get; private set; }

        /// <summary>
        /// The filtering method of the input filter
        /// </summary>
        public Method FilteringMethod { get; set; }

        /// <summary>
        /// Raised when an invalid key has been detected
        /// </summary>
        public event Action<KeyCode> InvalidKeyReceived;

        private KeyCode[] keysList;
        private HashSet<KeyCode> keysHashSet;

        /// <summary>
        /// Creates an inactive empty filter
        /// </summary>
        internal InputFilter() : 
            this(new List<KeyCode>().ToArray(), Method.Whitelist)
        { }

        /// <summary>
        /// Creates a filter, if the array of keys contains at least one key, it will activate the filter
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="filteringMethod"></param>
        internal InputFilter(KeyCode[] keys, Method filteringMethod = Method.Whitelist)
        {
            FilteringMethod = filteringMethod;
            SetList(keys);
        }

        /// <summary>
        /// Derermines the validity of a given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>true if the key is valid</returns>
        internal bool IsKeyValid(KeyCode key)
        {
            if (FilteringActive)
            {
                bool valid = 
                    FilteringMethod == Method.Whitelist ?
                    keysHashSet.Contains(key) :
                    !keysHashSet.Contains(key);

                if (!valid)
                {
                    InvalidKeyReceived(key);
                    return false;
                }
            }
            return true;
        }

        private void SetList(KeyCode[] keysList)
        {
            this.keysList = keysList;
            keysHashSet = new HashSet<KeyCode>(keysList.Distinct());
            FilteringActive = keysHashSet != null && keysHashSet.Count > 0;
        }
    }
}