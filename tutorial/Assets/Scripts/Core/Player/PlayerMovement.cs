using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float turningRate = 180f;

    private Vector2 previousMovementInput;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            return;
        }

        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner)
        {
            return;
        }

        inputReader.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        float zRotation = previousMovementInput.x * (-turningRate) * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        rb.velocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
}
