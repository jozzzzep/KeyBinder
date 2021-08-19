using UnityEngine;
using TMPro;
using KeyBinder;

public class ExampleScript04 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootingForce;

    KeyCode currentBind;
    bool binding;

    void Start()
    {
        UpdateKeyBind(KeyCode.Space);
    }

    void Update()
    {
        if (binding) return;

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
        binding = true;
    }

    void DetectedNewKey(KeyCode key)
    {
        UpdateKeyBind(key);
        binding = false;
    }
}
