using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float moveSpeed = 15f; // Kecepatan gerakan background
    public float destroyXPosition = -10f; // Posisi X di mana background akan dihancurkan
    private GenerateBG backgroundGenerator; // Referensi ke BackgroundGenerator

    private void Start()
    {
        // Mencari komponen BackgroundGenerator di scene
        backgroundGenerator = FindObjectOfType<GenerateBG>();
    }

    private void Update()
    {
        // Gerakkan background ke kiri
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Hancurkan background jika sudah mencapai posisi X tertentu
        if (transform.position.x < destroyXPosition)
        {
            // Panggil metode untuk generate background baru
            backgroundGenerator.GenerateBackgroundAtPivot();
            Destroy(gameObject);
        }
    }
}
