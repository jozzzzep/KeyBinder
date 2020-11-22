# KeyBinder
An easy to use, all-in-one class for a key-binding system in Unity.  
Supports input filtering (You can choose wich keys are valid for binding).
- **Content**
  - [Setup](#setup)
  - [Input Filtering System](#input-filtering-system)
  - [Properties](#properties)
  - [Methods](#methods)


# Setup
  
- Before everything, create and initialize a KeyBinder
```csharp
KeyBinder keyBinder = new KeyBinder();
```

# Input Filtering System
  - There's a **list** of "valid keys" inside a **KeyBinder** object
  - If the **list's empty**, the input filtering will **not work** (Every key the user will press will be valid)
  - If the filtering's is **working** it'll return the received key **only if** it is **inside that list**
  - You can **add keys** to the list with these methods:
  ```csharp
  AddValidKey(KeyCode)
  AddValidKeys(KeyCode[])
  AddValidKeys(List<KeyCoNde>)
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

## Properties
```csharp
LatestKey            // Returns the latest key the KeyBinder received.
IsActive             // Determines if the KeyBinder is currently checking for input.
KeyFilteringActive   // Determines if the KeyBinder will filter the input
```

## Methods
```csharp
Update() // Call this on Update() inside a MonoBehaviour

InputCheckingBeginSingle()
InputCheckingBeginContinuous()
InputCheckingCancel()
InputCheckingPause()
InputCheckingResume()

AddValidKey()
AddValidKeys()
ClearValidKeys()
```
