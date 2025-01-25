using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementCharacter : MonoBehaviour
{
    // Inisiasi
    public float moveSpeed = 2f;
    private Animator anim;
    private Coroutine moveCoroutine;
    private bool isMovingUp = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Tombol K movement
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            isMovingUp = !isMovingUp;
            anim.SetBool("movement", true);

            float targetY = isMovingUp ? transform.position.y + 5f : transform.position.y - 5f;
            moveCoroutine = StartCoroutine(MoveToPosition(targetY));
        } else 
        {
            anim.SetBool("movement", false);
        } 
        
        // Tombol L untuk menyerang
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (moveCoroutine != null) StopCoroutine (moveCoroutine);
            anim.SetBool("movement", false);

            Debug.Log("Serang dia!");
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
