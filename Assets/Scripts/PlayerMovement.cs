using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float forwardSpeed = 8f;
    float sideSpeed = 6f;

    float currentSideInput;
    float sideVelocity;

    float normalSideSmoothTime = 0.1f;
    float currentSideSmoothTime = 0.1f;

    Rigidbody rb;
    Vector2 moveInput;

    float originalForwardSpeed;

    Coroutine speedBoostCoroutine;
    Coroutine snowCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;
    }

    public void ActivateSpeedBoost(float boostSpeed, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(boostSpeed, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostSpeed, float duration)
    {
        forwardSpeed = boostSpeed;

        yield return new WaitForSeconds(duration);

        forwardSpeed = originalForwardSpeed;
        speedBoostCoroutine = null;
    }

    public void ActivateSnowBlock(float forwardSlowAmount, float sideSlowAmount, float duration)
    {
        if (snowCoroutine != null)
        {
            StopCoroutine(snowCoroutine);
        }

        snowCoroutine = StartCoroutine(SnowBlockRoutine(forwardSlowAmount, sideSlowAmount, duration));
    }

    private IEnumerator SnowBlockRoutine(float forwardSlowAmount, float sideSlowAmount, float duration)
    {
        forwardSpeed = originalForwardSpeed - forwardSlowAmount;
        sideSpeed = sideSpeed - sideSlowAmount;
        currentSideSmoothTime = 0.3f;

        yield return new WaitForSeconds(duration);

        forwardSpeed = originalForwardSpeed;
        sideSpeed = 6f;
        currentSideSmoothTime = normalSideSmoothTime;

        snowCoroutine = null;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        currentSideInput = Mathf.SmoothDamp(
            currentSideInput,
            moveInput.x,
            ref sideVelocity,
            currentSideSmoothTime
        );

        Vector3 movement = new Vector3
        (
            currentSideInput * sideSpeed,
            rb.linearVelocity.y,
            forwardSpeed
        );

        rb.linearVelocity = movement;
    }

    void Update()
    {

    }
}