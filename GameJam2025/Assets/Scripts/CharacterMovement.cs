using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed= 5f;
    private bool isMovingUp = false;

    void Update()
    {

    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isMovingUp = true;
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            isMovingUp = false;
        }
        
        // Variable isMovingUp
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        } else if (!isMovingUp)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (!isMovingUp && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.L))
        {
            
        }
    }
}
