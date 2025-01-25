using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Kecepatan gerakan

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
