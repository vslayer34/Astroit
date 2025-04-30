using UnityEngine;

public class PointsHolder : MonoBehaviour
{
    private int _pointValue;
    
    [SerializeField]
    private int _basePointValue = 100;



    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        _pointValue = Mathf.RoundToInt(_basePointValue * transform.localScale.x);
    }

    // Getters & Setters---------------------------------------------------------------------------

    public int PointValue { get => _pointValue; }
}
