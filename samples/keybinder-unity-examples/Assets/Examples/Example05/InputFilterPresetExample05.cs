using UnityEngine;
using KeyBinder;

public class InputFilterPresetExample05 : IInputFilterPreset
{
    public KeyCode[] KeysList => new KeyCode[] 
    {
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.X,
        KeyCode.Q,
        KeyCode.W
    };

    public InputFilter.Method FilteringMethod => InputFilter.Method.Whitelist;
}
