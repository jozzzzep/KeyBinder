using System;
using UnityEngine;

namespace KeyBinder
{
    public class KeyDetector : MonoBehaviour
    {


        /// An easy to use class for a key-binding system in Unity
        /// Supports input filtering (You can choose wich keys are valid for binding)
        ///
        /// WIKI & INFO: https://github.com/JosepeDev/KeyBinder
        /// 
        /// IMPORTANT >>> Call this class's "Update()" on Update() inside a MonoBehaviour
        /// 
        /// Properties:
        /// - LatestKey
        /// - ValidKeys
        /// - IsActive
        /// - InputFilteringActive
        /// 
        /// Methods:
        /// - InputCheckingBeginSingle (Action<KeyCode>)
        /// - InputCheckingBeginContinuous (Action<KeyCode>)
        /// - InputCheckingStop ()
        /// - InputCheckingPause ()
        /// - InputCheckingResume ()
        /// - InputFilteringAdd (KeyCode key)
        /// - InputFilteringAdd (KeyCode[] keys)
        /// - InputFilteringAdd (List<KeyCode> keys)
        /// - InputFilteringRemove (KeyCode key)
        /// - InputFilteringRemove (KeyCode[] keys)
        /// - InputFilteringRemove (List<KeyCode> keys)
        /// - InputFilteringRemoveAll ()

        private static KeyDetector _instance;
        private static KeyDetector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Initialize();
                return _instance;
            }
        }

        private static KeyDetector Initialize()
        {
            var instance = FindObjectOfType<KeyDetector>();
            if (instance == null)
            {
                var obj = new GameObject("KeyBinder");
                instance = obj.AddComponent<KeyDetector>();
                instance.inputFilter = new InputFilter();
            }
            return instance;
        }

        private static Action<KeyCode> singleDetectionAction;
        private static bool singleDetectionActive;
        private static InputFilter InputFilter => Instance.inputFilter;
        private InputFilter inputFilter;

        public static event Action<KeyCode> KeyReceived;

        /// <summary>
        /// Returns the latest key the <see cref="KeyDetector"/> received.
        /// </summary>
        static public KeyCode LatestKey => Instance.latestKey;
        private KeyCode latestKey = KeyCode.None;

        /// <summary>
        /// Determines if the <see cref="KeyDetector"/> is currently checking for input.
        /// </summary>
        static public bool InputCheckIsActive => Instance.isActive;
        bool isActive = false;

        private void Update()
        {
            // if the input checking is active
            // it checks for input
            // when a key is pressed it calls ReceiveInput().
            if (isActive)
                if (Input.anyKey)
                    ReceiveInput();
        }

        /// <summary>
        /// Waits until a key is pressed, calls the action, and turns off the input checking
        /// </summary>
        public static void InputCheckOnce(Action<KeyCode> action)
        {
            singleDetectionAction = action;
            singleDetectionActive = true;
            InputCheckSetActive(true);
        }

        /// <summary>
        /// Set whether you want to check for input
        /// </summary>
        public static void InputCheckSetActive(bool active) =>
            Instance.isActive = active;

        /// <summary>
        /// Removes all the listeners from the <see cref="KeyReceived"/> event
        /// </summary>
        public static void RemoveAllListeners() =>
            KeyReceived = null;


        // Returns the KeyCode of the current pressed key
        KeyCode GetPressedKey()
        {
            var keysArray = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            for (int i = 0; i < keysArray.Length; i++)
                if (Input.GetKeyDown(keysArray[i]))
                    return keysArray[i];
            return KeyCode.None;
        }

        // Returns the pressed key
        void ReceiveInput()
        {
            KeyCode thePressedKey = GetPressedKey();
            if (thePressedKey != KeyCode.None)
            {
                if (inputFilter.IsKeyValid(thePressedKey))
                {
                    latestKey = thePressedKey;
                    OnKeyReceived(thePressedKey);
                }
            }
        }

        private static void OnKeyReceived(KeyCode key)
        {
            var e = KeyReceived;
            if (e != null)
                e(key);

            if (singleDetectionActive)
            {
                if (singleDetectionAction != null)
                    singleDetectionAction(key);
                singleDetectionAction = null;
                InputCheckSetActive(false);
                singleDetectionActive = false;
            }
        }
    }
}
