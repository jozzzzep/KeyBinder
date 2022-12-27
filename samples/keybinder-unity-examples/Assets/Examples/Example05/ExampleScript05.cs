using UnityEngine;
using TMPro;
using KeyBinder;
using System.Text;

public class ExampleScript05 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp, filteredKeysTextComp;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootingForce;

    KeyCode currentBind;

    void Start()
    {
        UpdateKeyBind(KeyCode.X);
        KeyDetector.SetInputFilter(new InputFilterPresetExample05());
        UpdateFilterText(KeyDetector.InputFilter);
    }

    void Update()
    {
        if (KeyDetector.InputCheckIsActive) return;

        if (Input.GetKeyDown(currentBind))
            Shoot();
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePoint.transform.position;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, shootingForce), ForceMode2D.Impulse);
    }

    void UpdateKeyBind(KeyCode key)
    {
        currentBind = key;
        textComp.text = $"Current shooting key: {key}";
    }

    public void BindNewKey()
    {
        textComp.text = $"Press a key to bind it";
        KeyDetector.InputCheckOnce(DetectedNewKey);
    }

    void DetectedNewKey(KeyCode key)
    {
        UpdateKeyBind(key);
    }

    void UpdateFilterText(InputFilter filter)
    {
        filteredKeysTextComp.text = "Input Filter Is Active\n" +
            "Filtering Method: " + filter.FilteringMethod.ToString() + "\n" +
            "Valid keys to bind:\n" +
            FilterKeysIntoString(filter.Keys);
    }

    string FilterKeysIntoString(KeyCode[] keys)
    {
        var builder = new StringBuilder();
        var lastKeyIndex = keys.Length - 1;
        for (int i = 0; i < keys.Length; i++)
        {
            builder.Append(keys[i].ToString());
            if (i != lastKeyIndex)
                builder.Append(", ");
        }
        return builder.ToString();
    }
}
