using UnityEngine;

public class MoveToBeatSmooth : MonoBehaviour
{
    public float bpm = 123f; // BPM dari lagu
    private float beatInterval;
    private float nextBeatTime;
    public Vector3 moveDirection = Vector3.left; // Arah gerakan objek
    public float moveDistance = 1f; // Jarak gerakan objek per ketukan
    private Vector3 targetPosition;

    private void Start()
    {
        beatInterval = 60f / bpm; // Hitung interval ketukan
        nextBeatTime = Time.time + beatInterval; // Waktu untuk ketukan berikutnya
        targetPosition = transform.position; // Set posisi awal sebagai target awal
    }

    private void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            // Set target position untuk ketukan berikutnya
            targetPosition += moveDirection * moveDistance;
            nextBeatTime += beatInterval; // Set waktu untuk ketukan berikutnya
        }

        // Gerakan halus menuju target position menggunakan Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / beatInterval);
    }
}
