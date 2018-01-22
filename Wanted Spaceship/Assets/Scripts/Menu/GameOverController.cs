using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text score;
    public Text highscore;

	void Start () {
        score.text = "Score: " + PlayerPrefs.GetInt("Score");
        if (PlayerPrefs.HasKey("HighScore")) {
            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            }
            highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
        else {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
	}
}
