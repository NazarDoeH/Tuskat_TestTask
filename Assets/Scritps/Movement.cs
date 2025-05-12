using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float speedMultiplier = 10.0f;
    [Header("Acceleration")]    
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float accelerationTime = 1f;
    
    private Rigidbody _rb;

    private Vector2 _movementInput;
    private float _accelerationTimer;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    //Movement input and Acceleration timer
    void Update()
    {
        _movementInput = GetInput();
        _accelerationTimer = _movementInput != Vector2.zero ?
            _accelerationTimer + Time.deltaTime / accelerationTime : 
            0.0f;
    }

    void FixedUpdate()
    {
        float curveValue = accelerationCurve.Evaluate(
            Mathf.Clamp01(_accelerationTimer));
        Vector2 acceleration = _movementInput.normalized * (curveValue * speedMultiplier);

        ApplyMovement(acceleration);
    }

    // Apply movement
    void ApplyMovement(Vector2 inputAcceleration)
    {
        ApplyRotation();
        Vector3 forceDirection = new Vector3(
            inputAcceleration.y,
            0,
            -inputAcceleration.x).normalized;
        Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        
        float projectedSpeed = Vector3.Dot(horizontalVelocity, forceDirection);
        if(projectedSpeed < maxSpeed)
            _rb.AddForce(forceDirection * inputAcceleration.magnitude, ForceMode.Acceleration);
    }

    //Apply rotation
    void ApplyRotation()
    {
        Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);

        if (horizontalVelocity.sqrMagnitude > 0.001f)
        {
            Vector3 lookDirection = new Vector3(horizontalVelocity.x, 0, -horizontalVelocity.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Euler(0, -targetRotation.eulerAngles.y, 0);
        }
    }
    
    protected abstract Vector2 GetInput();
}
