using System.Collections;
using UnityEngine;

public class StopParallax : MonoBehaviour
{
    public float stopDuration = 1f; // Durasi untuk menghentikan parallax secara smooth
    public Paralax_BG parallaxScript; // Referensi ke script Paralax_BG

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            StartCoroutine(SmoothStopParallax());
        }
    }

    private IEnumerator SmoothStopParallax()
    {
        float initialSpeed = parallaxScript.speed;
        float elapsedTime = 0f;

        while (elapsedTime < stopDuration)
        {
            parallaxScript.speed = Mathf.Lerp(initialSpeed, 0f, elapsedTime / stopDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parallaxScript.speed = 0f;
    }
}
