using UnityEngine;

public class MoveLeftToBeat : MonoBehaviour
{
    public float bpm = 123f; // BPM dari lagu
    private float beatInterval; // Interval ketukan dalam detik
    public float moveDistancePerBeat = 1f; // Jarak gerakan per ketukan dalam unit
    private float moveSpeed; // Kecepatan gerakan per detik
    private Vector3 targetPosition; // Posisi target untuk gerakan

    private void Start()
    {
        beatInterval = 60f / bpm; // Hitung interval ketukan
        moveSpeed = moveDistancePerBeat / beatInterval; // Hitung kecepatan gerakan
        targetPosition = transform.position; // Set posisi awal sebagai target awal
    }

    private void Update()
    {
        // Hitung posisi target berikutnya berdasarkan kecepatan gerakan dan interval ketukan
        targetPosition += Vector3.left * (moveSpeed * Time.deltaTime);

        // Gerakkan objek secara halus menuju target position menggunakan Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / beatInterval);
    }
}
