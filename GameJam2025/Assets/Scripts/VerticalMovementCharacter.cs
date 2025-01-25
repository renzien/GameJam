using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementCharacter : MonoBehaviour
{
    // Inisiasi
    public float moveSpeed = 2f;
    private Animator anim;
    private Coroutine moveCoroutine;
    private bool isHoldingAttack = false;
    private bool isMovingUp = false;
    private bool isAttacking = false;

    // Main Method
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Gerakan ke atas dan ke bawah dengan tombol K
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(MoveCharacter()); // Pindahkan karakter dengan coroutine
        }
    
        // Tombol L menyerang renew
        // if (Input.GetKeyDown(KeyCode.L) && !isHoldingAttack)
        // {
        //     NormalAttack();
        // }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isHoldingAttack && !isAttacking)
            {
                NormalAttack();
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (!isHoldingAttack)
            {
                StartHoldAttack();
            }
        } else if (isHoldingAttack)
        {
            EndHoldAttack();
        }
    }

    // Method Function
    private IEnumerator MoveCharacter()
    {
        anim.SetBool("movement", true); 
        anim.SetBool("isMovingUp", !isMovingUp); 
        anim.SetBool("isMovingDown", isMovingUp); 

        float targetY = isMovingUp ? transform.position.y - 5f : transform.position.y + 5f; 
        float startY = transform.position.y; 
        float journeyLength = Mathf.Abs(targetY - startY); 
        float journeyTime = journeyLength / moveSpeed; 
        float elapsedTime = 0f; 

        // Gerakan ke posisi target
        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime; 
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / journeyTime); 
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); 
            yield return null; 
        }

        
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        anim.SetBool("movement", false); 
        anim.SetBool("isMovingUp", false); 
        anim.SetBool("isMovingDown", false); 

        // Update status gerakan
        isMovingUp = !isMovingUp; 
    }

    private IEnumerator ResetAttackStatus()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void StartHoldAttack()
    {
        isHoldingAttack = true;
        Debug.Log("Hold Attack kita mulai");
    }

    private void EndHoldAttack()
    {
        isHoldingAttack = false;
        Debug.Log("Selesai Hold Attack");
    }

    private void NormalAttack()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        Debug.Log("Serangan biasa dilakukan");

        StartCoroutine(ResetAttackStatus());
    }

    private void MoveUp()
    {
        anim.SetBool("movement", true); 
        transform.position += new Vector3(0, 5f, 0);
        isMovingUp = true; 
    }

    private void MoveDown()
    {
        anim.SetBool("movement", false); 
        transform.position += new Vector3(0, -5f, 0); 
        isMovingUp = false; 
    }
}
