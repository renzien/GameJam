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

    private float holdTime = 0.0f;
    private const float holdThreshold = 0.5f;

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
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     if (!isHoldingAttack && !isAttacking)
        //     {
        //         NormalAttack();
        //     }
        // }

        //Tombol L dengan holder
        if (Input.GetKeyDown(KeyCode.L))
        {
            holdTime = 0.0f;
        }

        // if (Input.GetKey(KeyCode.L))
        // {
        //     if (!isHoldingAttack)
        //     {
        //         StartHoldAttack();
        //     }
        // } else if (isHoldingAttack)
        // {
        //     EndHoldAttack();
        // }

        //Tombol GetKey
        if (Input.GetKey(KeyCode.L))
        {
            holdTime += Time.deltaTime;
            if (!isHoldingAttack && !isAttacking)
            {
                if (holdTime >= holdThreshold)
                {
                    StartHoldAttack();
                }
            }
             
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (holdTime < holdThreshold)
            {
                NormalAttack();
            }

            //Reset Waktunya
            holdTime = 0.0f;
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
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttacking",false);
        isAttacking = false;
    }

    private IEnumerator EndHoldAttack(float duration)
    {
        float elapsedTime = 0f; // Waktu yang telah berlalu
        while (elapsedTime < duration)
        {
            if (!Input.GetKey(KeyCode.L)) // Cek jika tombol tidak ditekan
            {
                break; // Keluar dari loop jika tombol dilepaskan
            }
            elapsedTime += Time.deltaTime; // Tambahkan waktu yang telah berlalu
            yield return null; // Tunggu frame berikutnya
        }
        anim.SetBool("isHoldingAttack", false); // Reset animasi serangan
        Debug.Log("Hold attack diakhiri");
    }

    private void StartHoldAttack()
    {
        anim.SetBool("isHoldingAttack", true); // Set animasi serangan hold
        Debug.Log("Serangan Hold");

        // Mulai coroutine untuk mengakhiri hold attack
        StartCoroutine(EndHoldAttack(1f)); // Misalnya, durasi hold attack 1 detik
    }

    private void NormalAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            StartCoroutine(ResetAttackStatus());
        }
    }

    private float GetAnimationDuration(string animationName)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName))
        {
            return stateInfo.length;
        }
        return 0f;
    }
}
