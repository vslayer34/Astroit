using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D _rb;

    private int _pointValue;

    private float _defaultScale;
    private const int POINT_VALUE = 100;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultScale = transform.localScale.x;
    }

    private void OnEnable()
    {
        transform.localScale = transform.localScale * Random.Range(0.5f, 1.5f);
        _pointValue = Mathf.RoundToInt(POINT_VALUE * transform.localScale.x);

        transform.position = Random.insideUnitCircle.normalized * 50.0f;
        
        _rb.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
        _rb.AddTorque(Random.Range(-10.0f, 10.0f));
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one * _defaultScale;
    }

    // Getters & Setters---------------------------------------------------------------------------

    public int PointValue { get => _pointValue; }
}
