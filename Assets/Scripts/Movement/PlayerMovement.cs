using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick = null;

    [SerializeField] private float gravity = 0.15f;
    [SerializeField] private float rayOffset = 1.05f;

    private float gravitySpeed = 0f;
    private float distanceToGtound = 0.05f;

    private CharacterController _characterController;
    private Transform _transform;

    private Vector3 motion = new Vector3();
    private Vector3 gravityMotion = new Vector3();

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Rotate();
        Gravity();
        Move();
    }

    private void Move()
    {
        motion = PlayerData.Player.Speed * _joystick.Movement * Time.deltaTime;
        _characterController.Move(Vector3.Scale(motion, _transform.forward));
    }

    private void Rotate()
    {
        _transform.rotation = _joystick.Rotation;
    }

    private void Gravity()
    {
        if (IsGrounded())
            gravitySpeed = 0f;
        else
            gravitySpeed -= gravity * Time.deltaTime;

        gravityMotion = -Vector3.down * gravitySpeed;
        _characterController.Move(gravityMotion);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(_transform.position + Vector3.down * rayOffset, Vector3.down, distanceToGtound);
    }
}
