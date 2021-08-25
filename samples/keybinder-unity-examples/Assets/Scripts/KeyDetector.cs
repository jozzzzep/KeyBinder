using System;
using UnityEngine;

namespace KeyBinder
{
    /// Source code & Documentation: https://github.com/JosepeDev/KeyBinder
    /// <summary>
    /// A class for detecting which keys are being press
    /// </summary>
    public class KeyDetector : MonoBehaviour
    {
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
        /// The input filter of the detector, inactive by default, add valid keys to activate filtering
        /// </summary>
        public static InputFilter InputFilter => Instance.inputFilter;

        /// <summary>
        /// Returns the latest key the <see cref="KeyDetector"/> received.
        /// </summary>
        public static KeyCode LatestKey => Instance.latestKey;

        /// <summary>
        /// Called when a key has been received by key detector
        /// </summary>
        public static event Action<KeyCode> KeyReceived;
        
        private InputFilter inputFilter = new InputFilter();
        private KeyCode latestKey = KeyCode.None;
        private bool keyCheckIsActive = false;
        private bool isInitialized = false;

        /// <summary>
        /// Waits until a key is pressed, calls the action, and turns off the input checking
        /// </summary>
        public static void InputCheckOnce(Action<KeyCode> action)
        {
            if (action == null) return;
            singleDetectionAction = action;
            InputCheckSetActive(true);
        }

        /// <summary>
        /// Set whether you want to check for input
        /// </summary>
        public static void InputCheckSetActive(bool active) =>
            Instance.keyCheckIsActive = active;

        /// <summary>
        /// Removes all the listeners from the <see cref="KeyReceived"/> event
        /// </summary>
        public static void RemoveAllListeners() =>
            KeyReceived = null;


        private static void OnKeyReceived(KeyCode key)
        {
            var e = KeyReceived;
            if (e != null)
                e(key);


            if (singleDetectionAction != null)
            {
                singleDetectionAction(key);
                singleDetectionAction = null;
                InputCheckSetActive(false);
            }
        }

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

        // Returns the pressed key
        private void ReceiveInput()
        {
            var key = GetPressedKey();
            if (key != KeyCode.None)
            {
                if (inputFilter.IsKeyValid(key))
                {
                    latestKey = key;
                    OnKeyReceived(key);
                }
            }
        }    
    }
}
