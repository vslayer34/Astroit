using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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



    [SerializeField, Header("Player Configs")]
    private bool _isTwoPlayerGame = true;

    [SerializeField]
    private float _playerSpawnOffset = 3.0f;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Color[] _playerColors = new Color[2];

    [SerializeField]
    private GameObject[] _playerWinScreen = new GameObject[2];

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
        Player.totalPlayersNumber = 0;
        int playersNum = _isTwoPlayerGame ? 2 : 1;

        for (int i = 0; i < playersNum; i++)
        {
            GameObject createdPlayer = Instantiate(_playerPrefab);

            if (_isTwoPlayerGame)
            {
                float xOffset = (i == 0) ? -1 * _playerSpawnOffset : _playerSpawnOffset;
                createdPlayer.transform.position = Vector2.right * xOffset;
            }
            else
            {
                createdPlayer.transform.position = Vector2.zero;
            }

            createdPlayer.GetComponentInChildren<SpriteRenderer>().color = _playerColors[i];
            _playersUI[i].UpdateUIColor(_playerColors[i]);
            _playersUI[i].gameObject.SetActive(true);
        }

        // Game Loop
        while (_isGameRunning)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }

        // End Game

        if (_isTwoPlayerGame)
        {
            int highestScore = 0;
            int winningPlayer = 0;

            for (int i = 0; i < playersNum; i++)
            {
                if (_playersScore[i] > highestScore)
                {
                    highestScore = _playersScore[i];
                    winningPlayer = i;
                }
            }

            _playerWinScreen[winningPlayer].GetComponent<Image>().color = _playerColors[winningPlayer];
            _playerWinScreen[winningPlayer].SetActive(true);
        }
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
