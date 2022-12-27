![img](https://i.imgur.com/zkpS6qA.png)  

An easy to use all-in-one class for a key-binding in Unity.  
Supports input filtering (You can choose which keys are valid for binding).
#### Content
  - **[Setup](#setup)**
  - **[Examples](#examples)**
  - **[Input Filtering System](#input-filtering-system)**
  - **[Documentations](#documentations)**
      - [Properties](#properties)
      - [Constructors](#constructors)
      - [Methods](#methods)


# Setup
  
- First, create and initialize a KeyBinder.  
Read [here](#input-filtering-system) about the input filtering system.  
Make sure you are using this class inside a MonoBehaviour
```csharp
KeyBinder keyBinder = new KeyBinder(); // A KeyBinder without input filtering
```
- Now, the most **IMPORTANT** thing:  
call the "Update()" method of the KeyBinder inside the Update() method of your MonoBehaviour
```csharp
private void Update()
{
  keyBinder.Update();
}
```
- Done. You're all set and can use the KeyBinder tool.
- See examples of use [here](#examples)

# Examples
Do what the [Setup](#setup) section says first if you want these examples to work
- Let's say you have a KeyCode variable for the Jump action in your game.  
And you check if this KeyCode is pressed, and if it is, your character jumps.  
Now you want to let the player choose a key for Jump.
  - Create a method that takes a KeyCode and assigns it to the Jump key.
  - Now create a Bind method, that'll start checking for the player input.
 
  ```csharp
  KeyBinder keyBinder = new KeyBinder();
  KeyCode jumpKey;
  
  // binds a given KeyCode to the Jump Key
  void BindToJump(KeyCode key)
  {
    jumpKey = key;
  }
  
  // when called, it'll assign the next pressed key to the jumpKey variable
  void CheckForInput()
  {
    keyBinder.InputCheckingBeginSingle(BindToJump);
  }
  
  void Update()
  {
    keyBinder.Update();
    if (Input.GetKeyDown(jumpKey))
    {
      Jump();
    }
  }
  ```
- Let's say you want every time the player presses a button, to display its name.
  - Create a variable for the text component.
  - Create a method that takes a KeyCode and prints it to the text component.
  - Create a method that should start checking for input.
  - Create a method that should stop checking for input.
  ```csharp
  KeyBinder keyBinder = new KeyBinder();
  public Text textComponent;
  
  // displays the name of a given KeyCode the text component
  void ShowKeyOnScreen(KeyCode key)
  {
    textComponent.text = key.ToString();
  }
  
  // when called, it'll start checking for input
  void StartCheckForInput()
  {
    // It'll endlessly call the method "ShowKeyOnScreen" every time the player presses a key.
    // To stop it, you should call the method InputCheckingStop inside the KeyBinder
    
    keyBinder.InputCheckingBeginContinuous(ShowKeyOnScreen);
  }
  
  // when called, it'll stop checking for input
  void StopCheckingForInput()
  {
    keyBinder.InputCheckingStop();
  }
  
  void Update()
  {
    keyBinder.Update();
  }
  ```

# Input Filtering System
  - There's a **list** of "valid keys" inside a **KeyBinder** object
  - If the **list's empty**, the input filtering will **not work** (Every key the user will press will be valid)
  - If the filtering is **working** it'll return the received key **only if** it is **inside that list**
  - You can **add keys** or **remove keys** from the list with these methods:
  ```csharp
  InputFilteringAdd (KeyCode)
  InputFilteringAdd (KeyCode[])
  InputFilteringAdd (List<KeyCode>)
  
  InputFilteringRemove (KeyCode)
  InputFilteringRemove (KeyCode[])
  InputFilteringRemove (List<KeyCode>)
  
  InputFilteringRemoveAll()
  ```
  - You can also add them upon **initialization** with **constructors**
  ```csharp
  // No input, empty list, no filtering
  KeyBinder keyBinder = new KeyBinder();
  
  // Adds the key from the array you entered to the "valid keys list"
  KeyBinder keyBinder = new KeyBinder(KeyCode[]);
  
  // Adds the key from the list you entered to the "valid keys list"
  KeyBinder keyBinder = new KeyBinder(List<KeyCode>); 
  ```
  - Array of keys example:
  ```csharp
  KeyCode[] keysArray =
  {
    KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
    KeyCode.G, KeyCode.H, KeyCode.I
  }
  KeyBinder keyBinder = new KeyBinder(keysArray); // initialized with input filtering
  ```

# Documentations

  - [KeyDetector](#keydetector)
  - [InputFilter](#global-audio-player)
  - [IInputFIlter](#global-audio-player-link)

## KeyDetector
The main class of the KeyBinder tool, used for detecting when a user/player presses a key.

- Properties
  - <code>InputCheckIsActive</code>  
  Set to false by default  
  Determines if the KeyDetector is currently checking for input  
  To activate see methods: **InputCheckOnce** and **InputCheckSetActive** bellow

  - <code>InputFilter</code>  
  The input filter of the detector  
  Filtering is disabled by default  
  To activate call - **SetInputFilter()**  
  To disable filtering call - **DisableInputFilter()**

  - <code>LatestKey</code>  
  Returns the latest valid key the KeyDetector received

- Events
  - ``KeyReceived (KeyCode) ``  
  Raised when a valid key has been received by the key detector

  - ``InvalidKeyReceived (KeyCode)``  
  Raised when an invalid key has been detected

- Methods
  - <code>InputCheckOnce (Action\<KeyCode\>)</code>  
  Waits until a key is detected, calls the action, and turns off input checking  
  Call this if you want to stop the input checking after a single key press  
  After the input detection, the Action you passed is removed from the event listeners  
  If the detector receives an invalid key, it will continue checking for input

  - <code>InputCheckSetActive (bool)</code>  
  Set manually whether you want to check for input, keeps checking **until deactivation**  
  Call this if you want to **keep checking** for the key the user/player is pressing  
  The KeyDetector will keep checking for input and will activate events until you deactivates the input checking manually

  - <code>RemoveAllListeners ()</code>  
  Removes all the listeners from the KeyReceived event

  - <code>SetInputFilter (InputFilter)</code>  
   Used to set custom filtering for the KeyDetector  
   Just call and input an InputFilter object  
   It is reccomended to use the method bellow instead

  - <code>SetInputFilter (IInputFilterPreset)</code>  
  Used to set custom filtering for the KeyDetector  
  Will create an input filter based on the preset you passed
  
  - <code>DisableInputFilter ()</code>  
  Disables input filtering  
  Sets the InputFilter of the detector to an empty filter

## InputFilter
A class the KeyDetector uses to filter input.  
That means that you can prevent the KeyDetector from detecting certain keys  
The KeyDetector has an empty inactive InputFilter by default (not filtering).  

If you want to use input filtering on the KeyDetector,  
you **need** to set it up **before** you check for input.  
The best practice it to do it inside the **Start()** or **Awake()** methods.  
See **[Examples](#input-filter-usage-examples)** 


- Properties
  - ``Keys``  
   An array of KeyCode, containing the keys the filter is using for filtering  
   If null/empty, the filter is inactive - see bellow

  - ``FilteringActive``  
  Returns a bool that determines whether the filter is filtering  
  When an input filter is created this value is initialized to **true** 
  or **false**  
  Only if the list of keys given to the filter is not empty and contains at least one key, the filter is activated (value set to **true**)
  
  - ``FilteringMethod``  
  Returns an enum of type **InputFilter.Method**  
  Can be either **Whitelist** or **Blacklist** - see bellow

  ### Filtering Method
  The key will be validated by the filter only if:
    - Whitelist   - it is inside the list of keys of the filter
    - Blacklist   - it is not inside the list of keys of the filter
     
    

### Input Filter Usage Examples
Lets say you want the KeyDetector to only detect the keys:  
**A, X, Q, Right Mouse Click, Left Mouse Click**  
You can do it like this:
```csharp
Start()
{
    // Create an array of KeyCodes or use an existing one
    // It should contain all the keys you want to filter
    KeyCode[] keysToFilter = 
    {
        KeyCode.A,
        KeyCode.X,
        KeyCode.Q,
        KeyCode.Mouse0, // left mouse button
        KeyCode.Mouse1  // right mouse button
    };

    // 1 - Create and allocate a new InputFilter
    // 2 - Pass the array of keys and choose filtering method
    InputFilter inputFilter = new InputFilter(keysToFilter, InputFilter.Method.Whitelist);

    // Call the method "SetInputFilter" and pass the filter 
    KeyDetector.SetInputFilter(inputFilter);
}
```
---
Also, if you want instead that the KeyDetector will only detect the keys that are **not** inside the list  
Just set the method to Blacklist like that:


```csharp
InputFilter inputFilter = new InputFilter(keysToFilter, InputFilter.Method.Blacklist);
```
---
Also, if you want to use the **Whitelist** filtering method  
You can call the constructor without specifing the method. Like so:

```csharp
InputFilter inputFilter = new InputFilter(keysToFilter);
```
---
### Advanced Filtering Guide
Altough it is more advanced, and may look a bit complex -  
It is recommended to use input filter presets instead of creating a new InputFilter with a constructor.  
This way is a lot more professional and organized.  
To do this you need to create an input filter preset **class**.  
And for this the KeyBinder tool has the interface **IInputFilterPreset**

### IInputFilterPreset
And interface used for creating a preset for an InputFilter  
Recommended name for a preset class is to start it with ``InputFilterPreset``  
Just create a class and implemet these members:
 
  - ``KeyCode[] KeysList { get; }``  
  An array of keys you want to be filtered

  - ``InputFilter.Method FilteringMethod { get; }``  
  The filtering method see [here](#filtering-method)

---
### Advanced Examples
Lets say you want the KeyDetector to only detect the keys:  
A, X, Q, Right Mouse Click, Left Mouse Click  
First create a new class for the InputFilterPreset:  
```csharp
using UnityEngine;
using KeyBinder;

// use the interface IInputFilterPreset
public class InputFilterPresetExample : IInputFilterPreset
{
    // implement the array of keys like this
    public KeyCode[] KeysList => new KeyCode[] 
    {
        KeyCode.A,
        KeyCode.X,
        KeyCode.Q,
        KeyCode.Mouse0, // left mouse button
        KeyCode.Mouse1, // right mouse button
    };

    // choose a filtering method
    public InputFilter.Method FilteringMethod => InputFilter.Method.Whitelist;
}
```
---
Now, you just need to call the method **SetInputFilter** in **Start()** or **Awake()** like so:
```csharp
Start()
{
    // Just create a new instance of the preset class we created before
    KeyDetector.SetInputFilter(new InputFilterPresetExample());
}
```

