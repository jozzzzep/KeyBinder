using UnityEngine;

public class DestroyWhenOutOfScreen : MonoBehaviour
{
    private void OnBecameInvisible() =>
        Destroy(gameObject);
}
