using UnityEngine;
using TMPro;
using KeyBinder;

public class ExampleScript02 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;

    // Start is called before the first frame update
    void Start()
    {
        KeyCode[] keysToFilter =
        {
            KeyCode.Escape,
            KeyCode.Numlock,
        };

        var f = new InputFilter(keysToFilter);
        textComp.text = "Press a key";
        KeyDetector.InputCheckSetActive(true);
    }

    private void Update()
    {
        textComp.text = $"Key pressed: {KeyDetector.LatestKey}";
    }
}
