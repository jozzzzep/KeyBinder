using UnityEngine;

namespace KeyBinder
{
    /// <summary>
    /// An interface for creating an InputFilterPreset class
    /// </summary>
    public interface IInputFilterPreset
    {
        /// <summary>
        /// The list of keys to filter
        /// </summary>
        KeyCode[] KeysList { get; }
        /// <summary>
        /// The filtering method of the filter
        /// </summary>
        InputFilter.Method FilteringMethod { get; } 
    }
}
