using UnityEngine;
using TMPro;
using KeyBinder;

public class ExampleScript01 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;

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
