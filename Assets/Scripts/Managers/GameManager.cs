using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public ObjectPool ExplosionPool { get; private set; }

    [SerializeField]
    private ObjectPool[] _asteroidsPools = new ObjectPool[3];

    private bool _isGameRunning = true;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private IEnumerator Start()
    {
        // start Game

        // Game Loop
        while (_isGameRunning)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }

        // End Game
    }

    // Member Methods------------------------------------------------------------------------------

    private void SpawnAsteroid()
    {
        int i = Random.Range(0, _asteroidsPools.Length);

        var newAsteroid = _asteroidsPools[i].GetPoolObject();
        newAsteroid.SetActive(true);
    }
}
