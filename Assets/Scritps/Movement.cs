using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float speedMultiplier = 10.0f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float accelerationTime = 1f;
    
    private Rigidbody _rb;

    private Vector2 _movementInput;
    private float _accelerationTimer;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
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

    void ApplyMovement(Vector2 inputAcceleration)
    {
        Vector3 forceDirection = new Vector3(
            inputAcceleration.y,
            0,
            -inputAcceleration.x).normalized;
        Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        
        float projectedSpeed = Vector3.Dot(horizontalVelocity, forceDirection);
        if(projectedSpeed < maxSpeed)
            _rb.AddForce(forceDirection * inputAcceleration.magnitude, ForceMode.Acceleration);
    }

    protected abstract Vector2 GetInput();
}
