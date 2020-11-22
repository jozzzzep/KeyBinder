using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinder
{
    /// An easy to use class for a key-binding system in Unity
    /// Supports input filtering (You can choose wich keys are valid for binding)
    ///
    /// WIKI: 
    /// 
    /// IMPORTANT >>> Call this class's "Update()" on Update() inside a MonoBehaviour
    /// 
    /// Properties:
    /// - LatestKey
    /// - IsActive
    /// - InputFilteringActive
    /// 
    /// Methods:
    /// - Update()
    /// - InputCheckingBeginSingle()
    /// - InputCheckingBeginContinuous()
    /// - InputCheckingCancel()
    /// - InputCheckingPause()
    /// - InputCheckingResume()
    /// - AddValidKey()
    /// - AddValidKeys()
    /// - ClearValidKeys()

    #region Variables & Properties

    /// <summary>
    /// Returns the latest key the <see cref="KeyBinder"/> received.
    /// </summary>
    public KeyCode LatestKey
    {
        get => _latestKey;
    }

    /// <summary>
    /// Determines if the <see cref="KeyBinder"/> is currently checking for input.
    /// </summary>
    public bool IsActive
    {
        get => _isActive;
    }

    /// <summary>
    /// <para> Determines if the <see cref="KeyBinder"/> will filter the input. </para>
    /// <para> The Filtering is active if you added at least one key </para>
    /// <para> To add keys to the filtering:
    /// <see cref="AddValidKey(KeyCode)"/>, <see cref="AddValidKeys(KeyCode[])"/></para>
    /// </summary>
    public bool InputFilteringActive
    {
        get => validKeys.Count >= 1;
    }

    // private variables
    bool continuous;
    bool _isActive;
    Action<KeyCode> onKeyInput;
    List<KeyCode> validKeys = new List<KeyCode>();
    KeyCode _latestKey;

    #endregion

    #region Constructors

    /// <summary>
    /// To initialize the <see cref="KeyBinder"/> class while adding valid keys for input filtering.
    /// </summary>
    public KeyBinder(KeyCode[] _validKeys)
    {
        if (_validKeys != null)
            AddValidKeys(_validKeys);
    }

    /// <summary>
    /// To initialize the <see cref="KeyBinder"/> class while adding valid keys for input filtering.
    /// </summary>
    public KeyBinder(List<KeyCode> _validKeys)
        : this(_validKeys.ToArray()) { }

    /// <summary>
    /// To initialize the <see cref="KeyBinder"/> class without input filtering. (can be added later)
    /// </summary>
    public KeyBinder() { }

    #endregion

    #region Methods

    /// <summary>
    /// IMPORTANT >>> Run this inside Update() in a <see cref="MonoBehaviour"/>
    /// </summary>
    public void Update()
    {
        // if the input checking is active
        // it checks for input
        // when a key is pressed it calls ReceiveInput().
        if (_isActive)
        {
            if (Input.anyKey)
            {
                ReceiveInput();
            }
        }
    }

    /// <summary>
    /// <para> Checks for the next pressed key, then calls the method you entered and stops checking. </para>
    /// <para> Make sure the method contains only one parameter of type <see cref="KeyCode"/>. </para>
    /// </summary>
    /// <param name="methodToActive">A method with one <see cref="KeyCode"/> parameter.</param>
    public void InputCheckingBeginSingle(Action<KeyCode> methodToActive)
    {
        onKeyInput = methodToActive;
        SetInputChecking(true);
        continuous = false;
    }

    /// <summary>
    /// <para> Checks for the next pressed key, then calls the method you entered until you cancel the input checking. </para>
    /// <para> Make sure the method contains only one parameter of type <see cref="KeyCode"/>. </para>
    /// </summary>
    /// <param name="methodToActive">A method with one <see cref="KeyCode"/> parameter.</param>
    public void InputCheckingBeginContinuous(Action<KeyCode> methodToActive)
    {
        onKeyInput = methodToActive;
        SetInputChecking(true);
        continuous = true;
    }

    /// <summary>
    /// Resets and turns off input checking.
    /// </summary>
    public void InputCheckingCancel()
    {
        onKeyInput = null;
        SetInputChecking(false);
        continuous = false;
    }

    /// <summary>
    /// <para> Pauses input checking. </para>
    /// <para> Don't forget to resume with <see cref="InputCheckingResume()"/>. </para>
    /// <para> Let's say you want to not get the pressed key if the mouse hovers on some button, just call this. </para>
    /// </summary>
    public void InputCheckingPause() => SetInputChecking(false);

    /// <summary>
    /// <para> Resumes input checking. </para>
    /// <para> Call this to resume input checking after you paused it with <see cref="InputCheckingPause()"/>. </para>
    /// </summary>
    public void InputCheckingResume() => SetInputChecking(true);

    /// <summary>
    /// If you want to add a key for the input filering list.
    /// </summary>
    /// <param name="key">The key you want to add</param>
    public void AddValidKey(KeyCode key)
    {
        validKeys.Add(key);
    }

    /// <summary>
    /// If you want to add multiple keys for the input filering list.
    /// </summary>
    /// <param name="keys">Array with the keys you want to add</param>
    public void AddValidKeys(KeyCode[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            AddValidKey(keys[i]);
        }
    }

    /// <summary>
    /// If you want to add multiple keys for the input filering list.
    /// </summary>
    /// <param name="keys">A list with the keys you want to add</param>
    public void AddValidKeys(List<KeyCode> keys)
    {
        AddValidKeys(keys.ToArray());
    }

    /// <summary>
    /// Clears the list of valid keys and turns of input filtering
    /// </summary>
    public void ClearValidKeys() => validKeys.Clear();

    // Returns the KeyCode of the current pressed key
    KeyCode GetPressedKey()
    {
        // loops through every keycode and
        // checks which one has been pressed in the last frame

        var keyToReturn = KeyCode.None;
        var keysArray = (KeyCode[])Enum.GetValues(typeof(KeyCode));
        for (int i = 0; i < keysArray.Length; i++)
        {
            if (Input.GetKeyDown(keysArray[i]))
            {
                keyToReturn = keysArray[i];
                continue;
            }
        }
        return keyToReturn;
    }

    // Returns the pressed key
    void ReceiveInput()
    {
        KeyCode thePressedKey = GetPressedKey();

        if (thePressedKey != KeyCode.None)
        {
            if (IsKeyValid(thePressedKey))
            {
                _latestKey = thePressedKey;

                if (!continuous)
                {
                    SetInputChecking(false);
                }

                if (onKeyInput != null)
                    onKeyInput(thePressedKey);
            }
        }
    }

    // Checks if the key is in the "valid keys" list (input filtering)
    bool IsKeyValid(KeyCode keyCode)
    {
        if (validKeys == null || validKeys.Count <= 0)
        {
            return true;
        }
        else
        {
            bool returnThis = false;
            for (int i = 0; i < validKeys.Count; i++)
            {
                if (validKeys[i] == keyCode)
                {
                    returnThis = true;
                    continue;
                }
            }
            return returnThis;
        }
    }

    private void SetInputChecking(bool active) => _isActive = active;

    #endregion
}