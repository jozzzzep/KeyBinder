![img](https://i.imgur.com/zkpS6qA.png)  

<p align="center">
        <img src="https://img.shields.io/codefactor/grade/github/jozzzzep/KeyBinder/main">
        <img src="https://img.shields.io/github/languages/code-size/jozzzzep/KeyBinder">
        <img src="https://img.shields.io/github/license/jozzzzep/KeyBinder">
        <img src="https://img.shields.io/github/v/release/jozzzzep/KeyBinder">
</p>
<p align="center">
        <img src="https://img.shields.io/github/followers/jozzzzep?style=social">
        <img src="https://img.shields.io/github/watchers/jozzzzep/KeyBinder?style=social">
        <img src="https://img.shields.io/github/stars/jozzzzep/KeyBinder?style=social">
</p>

An easy to use tool for key-binding in Unity.  
Supports input filtering (You can choose which keys are valid for binding).  
The tool has detaild documentation, and simple examples of usage.  

#### Content 
- **[Documentation](#documentation-content)**  
- **[Examples & Guide](#examples-and-guide)**
- **[Importing](#importing-guide)**

## Documentation Content:
![img](https://i.imgur.com/swFyjTR.png)

  - [KeyDetector](#keydetector)
  - [Input Filtering](#input-filtering)
    - [**InputFilter** class members](#inputfilter-class-members)
    - [Filtering Methods](#filtering-method)
    - [Filtering Guide](#input-filter-usage-examples)
    - [**Advanced Filtering Guide**](#advanced-filtering-guide)
      - [Filter Presets](#iinputfilterpreset)
      - [Advanced Filtering Examples](#advanced-filtering-examples)

## KeyDetector
The main class of the KeyBinder tool, used for detecting when a user/player presses a key.  
All these members are static, so you don't need to create an instance of a KeyDetector.  

- **Properties**
  - <code>InputCheckIsActive</code>  
  Set to false by default  
  Determines if the KeyDetector is currently checking for input  
  To activate see methods: **InputCheckOnce** and **InputCheckSetActive** bellow

  - <code>InputFilter</code>  
  The input filter of the detector  
  Filtering is disabled by default  
  To activate read about [Input Filtering](#input-filtering)  
  To disable filtering call - **DisableInputFilter()**

  - <code>LatestKey</code>  
  Returns the latest valid key the KeyDetector received

- **Events**
  - ``KeyReceived (KeyCode) ``  
  Raised when a valid key has been received by the key detector

  - ``InvalidKeyReceived (KeyCode)``  
  Raised when an invalid key has been detected  
  Will be raised only if there is an active input filtering  
  When the InputFilter detects an invalid key it will raise this event


- **Methods**
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

# Input Filtering
For input filtering there is a class named **InputFilter**, a class the KeyDetector uses to filter input.  
That means that you can prevent the KeyDetector from detecting certain keys  
The KeyDetector has an empty inactive InputFilter by default (not filtering).  

If you want to use input filtering on the KeyDetector,  
you **need** to set it up **before** you check for input.  
The best practice it to do it inside the **Start()** or **Awake()** methods.  
See **[Examples](#input-filter-usage-examples)** 


### InputFilter class members:
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
     
    

### Input Filter Usage Guide
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

## Advanced Filtering Guide
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
### Advanced Filtering Examples
Lets say you want the KeyDetector to only detect the keys:  
A, X, Q, Right Mouse Click, Left Mouse Click  
First open a new file and create a new class for the InputFilterPreset:  
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
Done! Now your KeyDetector uses a filter based on the preset class you created.

### Naming a filter preset
Name the class of the preset by starting with **"InputFilterPreset"** and then add anything you want  
In this case I added **"Example"** and named it **"InputFilterPresetExample"**
  


# Examples and Guide

This tool can be used in many ways.  
Here are some examples:  

- [**Example #1**](#example-1)
- [**Example #2**](#example-2)
- [**Unity Examples**](#unity-examples)

## Example #1
You can can check which key the player/user pressed recently.  
To to that, turn on input checking in the KeyDetector class.  
After checking, you can get the key that was pressed from the property "LatestKey"  
You can write the key name to the console like so:  
```csharp
using KeyBinder; // important to use the KeyBinder namespace
using UnityEngine;

public class ExampleDebugScript : MonoBehaviour
{
    void Start()
    {
        KeyDetector.InputCheckSetActive(true); // turns on input checking
    }

    void Update()
    {
        Debug.Log(KeyDetector.LatestKey); // logs to the console the latest pressed key
    }
}
```

### Improve Efficiency
You can make it that it logs the key every time the detector detects a new key  
Instead of updating every frame the latest key.  
Do this inside the class ( no need for **Update()** ):

```csharp
void Start()
{
    KeyDetector.InputCheckSetActive(true); // turns on input checking

    // adds the method "DebugKey" to the event "KeyReceived"
    // it will call the method "DebugKey" every time a new key is detected
    KeyDetector.KeyReceived += DebugKey; 
}

void DebugKey(KeyCode keyCode)
{
    Debug.Log(KeyDetector.LatestKey); // logs to the console the latest pressed key
}
```

## Example #2
Let's say you have a game that uses a KeyCode variable for the shoot key.  
Whenever the player presses the shoot key, the game will shoot a bullet.  
And by default the shoot key is the key **X**:

```csharp
KeyCode shootKey = KeyCode.X;

void Update()
{
    if (Input.GetKeyDown(shootKey))
    {
        Shoot()
    }
}

void Shoot()
{
    // shooting behavior
}

```

Now you want to let the player choose the key he wants to use for shooting.  
To do that add a method that sets the "shootKey" variable to a new KeyCode.

```csharp
void SetShootKey(KeyCode key)
{
    shootKey = key;
}
```
Add the namespace "KeyBinder" at the top of the script.

```csharp
using KeyBinder;
```

You already have a method for setting the keybind of the shoot key.  
Nowreate a new method named BindShootKey().  
Inside, call the function "InputCheckOnce" and pass the method SetShootKey as a parameter.

```csharp
void BindShootKey()
{
    KeyDetector.InputCheckOnce(SetShootKey);
}
```
Now call this method every time you want the player to change the key for shooting.  
It will wait until a key is pressed, and will call the function "SetShootKey" with the new key.  
It will also automatically stops checking for input after the key is pressed.  
So you don't need to worry about stoping the input checking manually.  

*You can also make the "BindShootKey" method - public, to assign it to a button in the UI.*

The KeyDetector also supports InputFiltering read about it [here](#input-filtering)


## Unity examples
Clone the repository  
In the folder "samples" you can find a unity project named "keybinder-unity-examples"  
Inside this unity project you have many examples that show you what you can do with the tool  

---
In the unity file browser, you will see these folders:

![img](https://i.imgur.com/h12lUij.png)

Then enter the "Examples" folder and you will see all the available examples

![img](https://i.imgur.com/hgMAz43.png)
 
In every example folder you will have a different scene that shows the example

![img](https://i.imgur.com/vgQz8Mt.png)

Now just open the scene and press play to see what happens.  
Inside the project you have 6 examples.  
You can learn from every example how the tool works.  

---

### Importing Guide
[**Click here to download**](https://github.com/jozzzzep/KeyBinder/raw/main/packages/KeyBinder.unitypackage), or go to the packages folder of the repository and download the package file.  
Then import the package directly to your project like any other Unity package.  
This is he fastest and easiest way.  
