using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoExt, IMovable, IRotatable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerMove = true;
    [TabGroup("Debug")] [SerializeField] private bool _canPlayerRotate = true;
    
    private Vector2 _movementInput = Vector2.zero;
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }
    
    public override void Initialize()
    {
        base.Initialize();
        _playerInput.EnablePlayerActions();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
    }

    public void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_canPlayerMove)
            return;
        
        // Get camera direction vectors
        Transform camTransform = Camera.main.transform;

        Vector3 camForward = camTransform.forward;
        camForward.y = 0;
        camForward.Normalize();
        
        Vector3 camRight = camTransform.right;
        camRight.y = 0;
        camRight.Normalize();
        
        Vector3 normalizedDirection = camRight * _movementInput.x +  camForward * _movementInput.y;
        normalizedDirection.Normalize();
        
        
        // Smooth Rotation
        OnPlayerRotate(normalizedDirection, _movementStats);
        OnPlayerMove(normalizedDirection, _movementStats);
    }

    public void OnPlayerMove(Vector3 movementDirection, MovementStats movementStats)
    {
        Vector3 velocity = movementDirection * movementStats.MovementSpeed;
        velocity.y = _rigidbody.linearVelocity.y; // Maintain original vertical velocity (gravity)
        
        _rigidbody.linearVelocity = velocity;
    }
    

    public void OnPlayerRotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }
}