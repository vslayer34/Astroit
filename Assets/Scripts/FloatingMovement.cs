using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    private Rigidbody2D _rb;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        transform.position = Random.insideUnitCircle.normalized * 50.0f;
        
        _rb.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
        _rb.AddTorque(Random.Range(-10.0f, 10.0f));
    }
}
