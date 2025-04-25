using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [field: SerializeField]
    public ObjectPool ExplosionPool { get; private set; }

    [SerializeField]
    private ObjectPool[] _asteroidsPools = new ObjectPool[3];

    [SerializeField]
    private ObjectPool[] _projectilePools = new ObjectPool[2];

    private bool _isGameRunning = true;

    [SerializeField]
    private float _SpawnTime = 2.0f;

    [SerializeField]
    private PlayerUI[] _playersUI;

    private int[] _playersScore = new int[2];



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

    public void ReportPlayerDeath(GameObject player, int playerNumber, int lives)
    {
        // Update UI
        _playersUI[playerNumber].UpdateLivesUI(lives);

        if (lives > 0)
        {
            StartCoroutine(RespawnPlayer(player));
            return;
        }
        else if (lives <= 0)
        {
            Player.totalPlayersNumber--;

            if (Player.totalPlayersNumber == 0)
            {
                _isGameRunning = false;
            }
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(_SpawnTime);
        player.transform.position = Vector2.zero;
        player.SetActive(true);
    }


    public void UpdatePlayersScore(int playerNumber, int newScore)
    {
        _playersScore[playerNumber] += newScore;
        _playersUI[playerNumber].UpdateScoreText(_playersScore[playerNumber]);
    }

    // Getters & Setters---------------------------------------------------------------------------

    public ObjectPool[] ProjectilePools { get => _projectilePools; }
}
