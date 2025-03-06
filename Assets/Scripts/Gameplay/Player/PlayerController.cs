using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoExt, IMovable, IRotatable, IAbilityCastable
{
    [TabGroup("References")] [SerializeField] private MovementStats _movementStats;
    [TabGroup("References")] [SerializeField] private PlayerInputReader _playerInput;
    [TabGroup("References")] [SerializeField] private Rigidbody _rigidbody;
    [TabGroup("References")] [SerializeField] private Camera _camera;

    [TabGroup("Ability")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("Ability")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    
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
        _abilityList.InitializeAbilities();
        _abilityParameterHandler.Initialize();
    }
     
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //Event that handles player movement
        AddEvent(_playerInput.Movement,movementDirection => _movementInput = movementDirection);
        AddEvent(_playerInput.Ability, OnAbilityCast);
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    //Handles movement and rotation
    private void HandleMovement()
    {
        if (!_canPlayerMove)
            return;
        
        Vector3 normalizedDirection = Utility.CalculateCameraDirection(_camera, _movementInput);
        
        Rotate(normalizedDirection, _movementStats);
        Move(normalizedDirection, _movementStats);
    }


    //Assigns care of players movement
    public void Move(Vector3 movementDirection, MovementStats movementStats)
    {
        Vector3 velocity = movementDirection * movementStats.MovementSpeed;
        velocity.y = _rigidbody.linearVelocity.y; // Maintain original vertical velocity (gravity)
        
        _rigidbody.linearVelocity = velocity;
    }
    

    //Assigns player rotations
    public void Rotate(Vector3 rotationDirection, MovementStats movementStats)
    {
        if (!_canPlayerRotate)
            return;
        
        if (rotationDirection.sqrMagnitude < 0.01f)
            return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, movementStats.RotationSmoothTime * Time.fixedDeltaTime));
    }

    public void OnAbilityCast(AbilityExtendableEnum abilityEnum)
    {
        _abilityList.AbilityDictionary[abilityEnum].OnTriggerAbility(gameObject, _abilityParameterHandler);
    }
}