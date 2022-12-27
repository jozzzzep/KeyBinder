using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/jozzzzep/KeyBinder
    /// <summary>
    /// The class that filters the input a <see cref="KeyDetector"/> receives
    /// </summary>
    public class InputFilter
    {
        /// Properties ------------
        /// - Keys                - The key's that are valid/invalid for the KeyDetector to receive
        /// - FilteringActive     - Determines whether the filter is filtering
        /// - FilteringMethod     - The filtering method the input filter uses
        /// -----------------------

        /// <summary>
        /// Determines the filtering method the <see cref="InputFilter"/> uses 
        /// </summary>
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
        /// Determines whether the key detector input filtering is active
        /// </summary>
        public bool FilteringActive { get; private set; }

        /// <summary>
        /// The filtering method the input filter uses
        /// </summary>
        public Method FilteringMethod { get; private set; }

        
        private KeyCode[] keysList;
        private HashSet<KeyCode> keysHashSet;

        /// <summary>
        /// Creates an inactive empty filter
        /// </summary>
        internal InputFilter() : 
            this(new List<KeyCode>().ToArray(), Method.Whitelist)
        { }

        /// <summary>
        /// Creates a filter from a preset
        /// </summary>
        internal InputFilter(IInputFilterPreset preset) :
            this(preset.KeysList, preset.FilteringMethod)
        { }

        /// <summary>
        /// Creates a filter, if the array of keys contains at least one key, it will activate the filter
        /// </summary>
        /// <param name="keys">List of keys to filter</param>
        /// <param name="filteringMethod"></param>
        internal InputFilter(KeyCode[] keys, Method filteringMethod = Method.Whitelist)
        {
            FilteringMethod = filteringMethod;
            keysList = keys;
            if (keysList != null)
            {
                keysHashSet = new HashSet<KeyCode>(keysList.Distinct());
                FilteringActive = keysHashSet != null && keysHashSet.Count > 0;
            }
            else
                FilteringActive = false;
        }

        /// <summary>
        /// Derermines the validity of a given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>true if the key is valid</returns>
        internal bool IsKeyValid(KeyCode key)
        {
            bool valid = true;
            if (FilteringActive)
            {
                valid = 
                    FilteringMethod == Method.Whitelist ?
                    keysHashSet.Contains(key) :
                    !keysHashSet.Contains(key);
            }
            return valid;
        }
    }
}
