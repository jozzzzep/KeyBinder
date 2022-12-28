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
    }

    private void Update()
    {
        textComp.text = $"Key pressed: {KeyDetector.LatestKey}";
    }
}
