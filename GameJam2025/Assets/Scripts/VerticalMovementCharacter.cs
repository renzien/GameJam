using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementCharacter : MonoBehaviour
{
    // Inisiasi
    public float moveSpeed = 2f;
    private bool isMovingUp = false;
    private Coroutine moveCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveToPosition(transform.position.y + 5f ));
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveToPosition(transform.position.y - 5f));
        }
    }

    private IEnumerator MoveToPosition(float targetY)
    {
        float startY = transform.position.y;
        float journeyLength = Mathf.Abs(targetY - startY);
        float journeyTime = journeyLength / moveSpeed;
        float elapsedTime = 0f;

        // Perulangan
        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / journeyTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        // Pastiin karakternya balik ke posisi awal.
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
