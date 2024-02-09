using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;

    public float MaxSpeed { get => _maxSpeed; }

    private float _maxSpeed = 7;
    private float _maxSpeedSqr;

    public float CurrentSpeed { get => Utils.GetXZMagnitudeFromVector(_rb.velocity); }

    public Vector3 CharacterForwardDirection { get => _rb.transform.forward; }

    [Min(1)] public float acceleration = 15;
    public float rotationSpeed = 1080; // Degrees per second
    private Vector3 _currentVelocityDamp; // ref velocity for the smoothDamp


    //[Header("Dashing")]
    //public float dashSpeed = 30;
    //public float dashTime = 0.2f;
    //public float dashCooldownTime = 2;

    //private bool _dashing;
    //private float _dashCooldown;



    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();

        _maxSpeedSqr = _maxSpeed * _maxSpeed;
    }

    public void PerformMovement(Vector2 movementDirection, Vector2 rotationDirection)
    {
        //UpdateLogic();

        Vector3 movementDirectionXZ = Utils.GetXZVectorFromInputVector(movementDirection);

        Move(movementDirectionXZ);
        Rotate(rotationDirection);

    }

    //private void UpdateLogic()
    //{
    //    // TODO move this out of her
    //    // Dash should be an ability on itself
    //    if (_dashCooldown > 0)
    //        _dashCooldown -= Time.fixedDeltaTime;
    //}


    private void Move(Vector3 movementDirection)
    {
        Vector3 targetVelocity = movementDirection * _maxSpeed;

        float smoothTime = _maxSpeed / acceleration;
        Vector3 velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _currentVelocityDamp, smoothTime);

        SetVelocity(velocity);
    }


    private void Rotate(Vector2 rotationInput)
    {
        if (rotationInput.x == 0 && rotationInput.y == 0)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(
            new Vector3(rotationInput.x, 0, rotationInput.y));

        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        _rb.MoveRotation(rotation);
    }


    //public void Dash(Vector3 movementDirection)
    //{
    //    Vector3 movementDirectionXZ = Utils.GetXZVectorFromInputVector(movementDirection);

    //    if (_dashCooldown <= 0)
    //    {
    //        if (movementDirectionXZ.magnitude < 0.1)
    //            movementDirectionXZ = _rb.transform.forward;

    //        _dashCooldown = dashCooldownTime;
    //        SetVelocity(movementDirectionXZ * dashSpeed);
    //    }

    //}

    public void SetPositionAndRotationFromTransform(Transform destinationTransform)
    {
        _rb.position = destinationTransform.position;
        _rb.rotation = destinationTransform.rotation;

    }

    public void SetVelocity(Vector3 velocity, bool overrideYVelocity = false)
    {
        Vector3 finalVelocity = new Vector3(
            velocity.x,
            (overrideYVelocity ? velocity.y : _rb.velocity.y),
            velocity.z
        );

        _rb.velocity = finalVelocity;

    }

    public void ResetVelocity()
    {
        Debug.Log("Resetting velocity");

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _currentVelocityDamp = Vector3.zero;

    }


    public void DisableCharacterController()
    {

        _rb.detectCollisions = false;
        //_collider.enabled = false;

        // Rigidbody speed variables
        ResetVelocity();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.Sleep();
    }

    public void EnableCharacterController()
    {

        //_collider.enabled = true;
        _rb.detectCollisions = true;

        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.WakeUp();
    }

}
