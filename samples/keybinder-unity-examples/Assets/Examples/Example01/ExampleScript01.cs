using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KeyBinder;

public class ExampleScript01 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;

    // Start is called before the first frame update
    void Start()
    {
        textComp.text = "Press a key";
        KeyDetector.InputCheckSetActive(true);
        KeyDetector.KeyReceived += KeyBinderKeyReceived;
    }

    private void KeyBinderKeyReceived(KeyCode obj)
    {
        textComp.text = $"Key pressed: {obj}";
    }
}
