using UnityEngine;

public class SizeRandomizer : MonoBehaviour
{
    private float _defaultScale;

    [SerializeField]
    private float _minSize = 0.5f;

    [SerializeField]
    private float _maxSize = 1.5f;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _defaultScale = transform.localScale.x;
    }

    private void OnEnable()
    {
        transform.localScale = transform.localScale * Random.Range(_minSize, _maxSize);
    }
}
