using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    [SerializeField] private Transform _targetTransform = null;

    [SerializeField] private float damping = 1.5f;

    private Transform _transform;

    private Vector3 offset = new Vector3();
    private Vector3 motionPosition = new Vector3();
    private Vector3 currentPosition = new Vector3();

    private void Start()
    {
        Initialization();
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Initialization()
    {
        _transform = GetComponent<Transform>();

        offset = _transform.position - _targetTransform.position;
    }

    private void Move()
    {
        motionPosition = _targetTransform.position + offset;
        currentPosition = Vector3.Lerp(_transform.position, motionPosition, damping * Time.deltaTime);

        _transform.position = currentPosition;
    }
}
