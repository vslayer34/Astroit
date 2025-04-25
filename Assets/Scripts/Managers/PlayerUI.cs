using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _livesText;



    // Member Methods------------------------------------------------------------------------------

    public void UpdateUIColor(Color playerColor)
    {
        _scoreText.color = playerColor;
        _livesText.color = playerColor;
    }

    public void UpdateLivesUI(int lives)
    {
        string livesText = string.Empty;
        
        switch (lives)
        {
            case 3:
                livesText = "^ ^ ^";
                break;
            
            case 2:
                livesText = "^ ^";
                break;
            
            case 1:
                livesText = "^";
                break;

            default:
                break;
        }

        _livesText.text = livesText;
    }


    public void UpdateScoreText(int score)
    {
        _scoreText.text = string.Format("{0:000000}", score);
    }
}
