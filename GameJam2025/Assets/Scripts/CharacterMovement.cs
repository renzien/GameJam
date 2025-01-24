using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float jumpHeight = 2.0f;
    public float jumpDuration = 0.5f;
    private Vector3 originalPosition;
    private bool isJumping = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isJumping)
        {
            Jump();
            Debug.Log("Ini dia loncat");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ReturnToOrigin();
            Debug.Log("Balik Lagi");
        }
    }

    void Jump()
    {
        Debug.Log("Loncat dia");
        isJumping = true;
        StartCoroutine(JumpCoroutine());
    }

    //bikin coroutine buat loncat
    private IEnumerator JumpCoroutine()
    {
        float elapsedtime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, jumpHeight, 0);

        while (elapsedtime < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedtime / jumpDuration));
            elapsedtime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isJumping = false;
    }

    void ReturnToOrigin()
    {
        transform.position = originalPosition;
    }
}
