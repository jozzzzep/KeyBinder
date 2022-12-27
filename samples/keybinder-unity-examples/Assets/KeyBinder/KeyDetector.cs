using System;
using UnityEngine;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/jozzzzep/KeyBinder
    /// <summary>
    /// A class for detecting which keys are being press
    /// </summary>
    public class KeyDetector : MonoBehaviour
    {
        /// Properties ---------------------------
        /// - InputCheckIsActive                 - Determines if the KeyDetector is currently checking for input
        /// - InputFilter                        - The input filter of the detector
        /// - LatestKey                          - Returns the latest key the KeyDetector received
        /// --------------------------------------
        /// 
        /// Events -------------------------------
        /// - KeyReceived(KeyCode)               - Raised when a valid key has been received by the key detector
        /// - InvalidKeyReceived(KeyCode)        - Raised when an invalid key has been detected
        /// --------------------------------------
        /// 
        /// Methods ------------------------------
        /// - InputCheckOnce(Action<KeyCode>)    - Waits until a key is detected, calls the action, and turns off input checking
        /// - InputCheckSetActive(bool)          - Set whether you want to check for input, keeps checking until deactivation
        /// - RemoveAllListeners()               - Removes all the listeners from the KeyReceived event
        /// ---                                ---
        /// - SetInputFilter(InputFilter)        - Used to set custom filtering for the KeyDetector
        /// - SetInputFilter(IInputFilterPreset) - Used to set custom filtering for the KeyDetector
        /// - DisableInputFilter()               - Disables input filtering
        /// --------------------------------------

        private static Action<KeyCode> singleDetectionAction;
        private static KeyDetector _instance;
        private static KeyDetector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Extensions.Initialize<KeyDetector>("KeyDetector");
                    if (!_instance.isInitialized)
                        KeyReceived = null;
                }
                return _instance;
            }
        }

        /// <summary>
        /// Determines if the <see cref="KeyDetector"/> is currently checking for input.
        /// </summary>
        public static bool InputCheckIsActive => Instance.keyCheckIsActive;

        /// <summary>
        /// The input filter of the detector, can be set by:
        /// <see cref="SetInputFilter(IInputFilterPreset)"/> and  <see cref="SetInputFilter(InputFilter)"/>
        /// </summary>
        public static InputFilter InputFilter => Instance.inputFilter;

        /// <summary>
        /// Returns the latest key the <see cref="KeyDetector"/> received.
        /// </summary>
        public static KeyCode LatestKey => Instance.latestKey;

        /// <summary>
        /// Raised when a valid key has been received by the key detector
        /// </summary>
        public static event Action<KeyCode> KeyReceived;

        /// <summary>
        /// Raised when an invalid key has been detected
        /// </summary>
        public static event Action<KeyCode> InvalidKeyReceived;

        private InputFilter inputFilter = new InputFilter();
        private KeyCode latestKey = KeyCode.None;
        private bool keyCheckIsActive = false;
        private bool isInitialized = false;

        /// <summary>
        /// Waits until a key is detected, calls the action, and turns off input checking
        /// </summary>
        public static void InputCheckOnce(Action<KeyCode> action)
        {
            if (action == null) return;
            singleDetectionAction = action;
            InputCheckSetActive(true);
        }

        /// <summary>
        /// Set whether you want to check for input, keeps checking until deactivation
        /// </summary>
        public static void InputCheckSetActive(bool active) =>
            Instance.keyCheckIsActive = active;

        /// <summary>
        /// Removes all the listeners from the <see cref="KeyReceived"/> event
        /// </summary>
        public static void RemoveAllListeners() =>
            KeyReceived = null;

        /// <summary>
        /// Used to set custom filtering for the <see cref="KeyDetector"/>
        /// </summary>
        /// <param name="filter">An <see cref="KeyBinder.InputFilter"/> object</param>
        public static void SetInputFilter(InputFilter filter) => Instance.inputFilter = filter ?? new InputFilter();

        /// <summary>
        /// Used to set custom filtering for the <see cref="KeyDetector"/>
        /// </summary>
        /// <param name="filterPreset">An <see cref="KeyBinder.IInputFilterPreset"/> object</param>
        public static void SetInputFilter(IInputFilterPreset filterPreset) =>
            Instance.inputFilter = (filterPreset != null) ? new InputFilter(filterPreset) : new InputFilter();

        /// <summary>
        /// Disables input filtering
        /// </summary>
        public static void DisableInputFilter() => SetInputFilter(new InputFilter());

        private static void OnKeyReceived(KeyCode key)
        {
            KeyReceived.SafeInvoke(key);
            if (singleDetectionAction != null)
            {
                singleDetectionAction(key);
                singleDetectionAction = null;
                InputCheckSetActive(false);
            }
        }

        private static void OnInvalidKeyReceived(KeyCode key) =>
            InvalidKeyReceived.SafeInvoke(key);

        private void Update()
        {
            // if the input checking is active
            // it checks for input
            // when a key is pressed it calls ReceiveInput().
            if (keyCheckIsActive)
                if (Input.anyKey)
                    ReceiveInput();
        }

        // Returns the KeyCode of the current pressed key
        private KeyCode GetPressedKey()
        {
            var keysArray = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            for (int i = 0; i < keysArray.Length; i++)
                if (Input.GetKeyDown(keysArray[i]))
                    return keysArray[i];
            return KeyCode.None;
        }

        private void ReceiveInput()
        {
            var key = GetPressedKey();
            if (key != KeyCode.None)
            {
                if (!inputFilter.IsKeyValid(key))
                {
                    OnInvalidKeyReceived(key);
                    return;
                }
                latestKey = key;
                OnKeyReceived(key);
            }
        }    
    }
}
