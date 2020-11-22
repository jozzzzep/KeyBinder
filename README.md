# KeyBinder
An easy to use, all-in-one class for a key-binding system in Unity.  
Supports input filtering (You can choose wich keys are valid for binding).
- **Content:**
  - [Setup](#setup)
  - [Properties](#properties)
  - [Methods](#methods)


# Setup
- Before everything, create and initialize a KeyBinder
```csharp
KeyBinder keyBinder = new KeyBinder();
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
