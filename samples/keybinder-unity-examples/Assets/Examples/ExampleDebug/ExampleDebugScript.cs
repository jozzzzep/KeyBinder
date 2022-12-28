using KeyBinder;
using UnityEngine;

public class ExampleDebugScript : MonoBehaviour
{
    void Start()
    {
        KeyDetector.InputCheckSetActive(true); // turns on input checking
        KeyDetector.KeyReceived += DebugKey;
    }

    void DebugKey(KeyCode keyCode)
    {
        Debug.Log(KeyDetector.LatestKey); // logs to the console the latest pressed key
    }
}
