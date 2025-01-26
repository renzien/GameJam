using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 0.5f; // Waktu untuk menghancurkan objek

    private void Start()
    {
        // Hancurkan objek setelah waktu yang ditentukan
        Destroy(gameObject, destroyTime);
    }
}
