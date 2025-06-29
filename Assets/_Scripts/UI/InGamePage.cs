using TMPro;
using UnityEngine;

public class InGamePage : MonoBehaviour
{
    public static InGamePage Instance;

    public TextMeshProUGUI scoreText;


    private int score;
    private void Awake()
    {
        Instance = this;
        score = 0;
    }


    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
