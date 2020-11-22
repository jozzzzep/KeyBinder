# KeyBinder
An easy to use, all-in-one class for a key-binding in Unity.  
Supports input filtering (You can choose wich keys are valid for binding).
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
See [here](#input-filtering-system) about the input filtering system.  
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
And you check if this KeyCode is pressed, and if it is, your charactar jumps.  
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
    if (Input.GetKeyDown(jumpKey)
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
  Text textComponent;
  
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
  - If the filtering's is **working** it'll return the received key **only if** it is **inside that list**
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
### Constructors
  - **KeyBinder ()**  
  No parameters enered.  
  Will create the KeyBinder with no valid keys (all keys are valid)  
  You can add valid keys later with methods.  
  
  - **KeyBinder (KeyCode[] validKeys)**  
  Enter an array of KeyCodes you choose to be valid for binding.  
  Will create the KeyBinder and add all the KeyCodes from your array to the list of valid keys.  
  You can add valid keys any time. 
  
  - **KeyBinder (List**<**KeyCode**> **validKeys)**  
  Enter a list of KeyCodes you choose to be valid for binding.  
  Will create the KeyBinder and add all the KeyCodes from the list you given, to the list of valid keys.  
  You can add valid keys any time. 
  
### Properties
  - **LatestKey**  
  Returns the latest key the KeyBinder received.  
  
  - **ValidKeys**  
  Returns the list of the valid keys as an array.  
  Returns null if list has 0 items.  
  
  - **IsActive**  
  Determines if the KeyBinder is currently checking for input.
  
  - **InputFilteringActive**  
  Determines if the KeyBinder will filter the input.  
  The Filtering is active if you added at least one key.
  
  
### Methods
  - **Update ()**  
  Call this method on Update() inside a MonoBehaviour inherited class.
  
  - **InputCheckingBeginSingle (Action**<**KeyCode**> **methodToActive)**  
  Enter a method with one parameter of type KeyCode as a parameter.  
  Checks for the next pressed key, then calls the method you entered, inputing the pressed key, and stops checking.  
  
  - **InputCheckingBeginContinuous (Action**<**KeyCode**> **methodToActive)**  
  Enter a method with one parameter of type KeyCode as a parameter.  
  Checks for the next pressed key, then calls the method you entered each time the user press a key until you cancel the input checking. 
  
  - **InputCheckingStop ()**  
  Resets and turns off input checking.  
  Use this to turn off the Continuous input checking.  
  
  - **InputCheckingPause ()**  
  Pauses input checking.  
  If you want to not check for input under a certain condition, you can.  
  Call this method, it'll keep the current checking data but will pause the checking until it resumed.  
  
  - **InputCheckingResume ()**  
  Resumes input checking.  
  Use it to resume the checking after you paused it.  
  
  - **InputFilteringAdd (KeyCode key)**  
  **InputFilteringAdd (KeyCode[] keys)**  
  **InputFilteringAdd (List**<**KeyCode**> **keys)**  
    
    Adds a single/bunch of KeyCodes to the list of valid keys.  
    You can call it if you wanna add a valid key after the initialization.  
    
  - **InputFilteringRemove (KeyCode key)**  
  **InputFilteringRemove (KeyCode[] keys)**  
  **InputFilteringRemove (List**<**KeyCode**> **keys)**  
    
    Removes a single/bunch of KeyCodes from the list of valid keys.  
    You can call it if you wanna remove a valid key after the initialization.  
    
  - **InputFilteringRemoveAll ()**  
  Clears the list of valid keys.  
  Makes it empty (makes every key valid and disables input filtering).  
  There's no need to use that if input filtering is already disabled.  
  You can call the property InputFilteringActive for checking if it's enabled.
  
