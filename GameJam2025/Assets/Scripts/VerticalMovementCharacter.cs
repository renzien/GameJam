using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VerticalMovementCharacter : MonoBehaviour
{
    // Inisiasi
    public float moveSpeed = 2f;
    public float moveDistance = 5f; // Tambahkan variabel publik untuk mengatur jarak pergerakan dari Inspector
    private Animator anim;
    private Coroutine moveCoroutine;
    private bool isHoldingAttack = false;
    private bool isMovingUp = false;
    private bool isAttacking = false;
    private bool isWallRunning = false;
    private bool isMoving = false; // Tambahkan variabel untuk mengecek status gerakan

    private float holdTime = 0.0f;
    private const float holdThreshold = 0.5f;

    private BoxCollider2D col;
    public AudioSource audioSource; // AudioSource untuk memutar audio
    public AudioClip attackClip; // Audio clip yang akan diputar saat menyerang

    // Main Method
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    void Update()
    {
        // Cek apakah karakter sedang bergerak
        if (!isMoving && Input.GetKeyDown(KeyCode.K))
        {
            if (isWallRunning)
            {
                isWallRunning = false;
                anim.SetBool("isWallRunning", false);
            }
            else
            {
                StartCoroutine(MoveCharacter());
            }
        }

        if (isMovingUp && Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(WallRun());
        }

        // Tombol L dengan holder
        if (Input.GetKeyDown(KeyCode.L))
        {
            holdTime = 0.0f;
        }

        // Tombol GetKey
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

            // Reset Waktunya
            holdTime = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Obstacle"))
        {
            // Memutar audio saat bertabrakan
            audioSource.PlayOneShot(attackClip);
        }
    }

    // Method Function
    private IEnumerator MoveCharacter()
    {
        isMoving = true; // Set status gerakan menjadi true
        anim.SetBool("movement", true);
        anim.SetBool("isMovingUp", !isMovingUp);
        anim.SetBool("isMovingDown", isMovingUp);

        float targetY = isMovingUp ? transform.position.y - moveDistance : transform.position.y + moveDistance;
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
        isMoving = false; // Set status gerakan menjadi false setelah gerakan selesai
    }

    private IEnumerator ResetAttackStatus()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
        col.enabled = false;
    }

    private IEnumerator EndHoldAttack(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (!Input.GetKey(KeyCode.L))
            {
                break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isHoldingAttack", false);
        Debug.Log("Hold attack diakhiri");
    }

    private IEnumerator WallRun()
    {
        isWallRunning = true;
        anim.SetBool("isWallRunning", true);
        yield return new WaitForSeconds(2f);

        // Matikan Wallrun
        isWallRunning = false;
        anim.SetBool("isWallRunning", false);
    }

    private void StartHoldAttack()
    {
        anim.SetBool("isHoldingAttack", true);
        Debug.Log("Serangan Hold");

        // Mulai coroutine untuk mengakhiri hold attack
        StartCoroutine(EndHoldAttack(1f));
    }

    private void NormalAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            col.enabled = true;

            // Pastikan parameter isAttacking dinonaktifkan setelah animasi selesai
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
