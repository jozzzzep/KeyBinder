# KeyBinder
An easy to use, all-in-one class for a key-binding system in Unity.  
Supports input filtering (You can choose wich keys are valid for binding).
[link](#methods)


# Setup
- Before everything, create and initialize a KeyBinder
```csharp
KeyBinder keyBinder = new KeyBinder();
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
