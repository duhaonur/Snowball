using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndGameScript : MonoBehaviour
{
    private float transition = 0.0f;

    private bool isLevelEnd = false;

    public Image backgroundImg;

    public GameObject levelEndPanel;
    public GameObject playButton;
    public GameObject retryButton;
    public GameObject failedText;
    public GameObject trail;

    public PlayerController playerController;
    public ScoreMultiplier scoreMultiplier;

    public Text scoreText;
    public Text gemIncreaseText;
    public Text gemCountText;
    private void Update()
    {
        if (!isLevelEnd)
            return;
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color32(0, 0, 0, 0), new Color32(6, 41, 12, 190), transition);
    }

    public void EndGame()
    {
        scoreText.enabled = true;
        playerController.endgame = true;
        scoreMultiplier.endgame = true;

        trail.SetActive(false);

        if(playerController.waterEnd == true)
        {
            playerController.steam.SetActive(false);
        }
        else
        {
            GameAnalyticsManager.Instance.LevelCompleted(PlayerPrefs.GetInt("CurrentLevel"));
            FacebookManager.Instance.LevelEnded(PlayerPrefs.GetInt("CurrentLevel"));
            playerController.steam.SetActive(true);
            playerController.player.velocity = playerController.player.velocity.normalized * 45;
        }
        playerController.frostGO.SetActive(false);
    }

    public IEnumerator SlowDownSnowBall()
    {
        if (playerController.waterEnd)
        {
            yield return new WaitForSeconds(1);
            isLevelEnd = true;
            levelEndPanel.SetActive(true);
            failedText.SetActive(true);
            yield return new WaitForSeconds(1);
            retryButton.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1);
            isLevelEnd = true;
            levelEndPanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gemIncreaseText.text = "+ " + playerController.collectedGems;
            gemIncreaseText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            PlayerPrefs.SetInt("GemCount", PlayerPrefs.GetInt("GemCount") + playerController.collectedGems);
            gemIncreaseText.enabled = false;
            gemCountText.text = PlayerPrefs.GetInt("GemCount").ToString();
            yield return new WaitForSeconds(1);
            playButton.SetActive(true);
        }
    }

    public void Score(float score, int endScoreMultiplier)
    {
        int x = (int)score * endScoreMultiplier;
        scoreText.text = x.ToString();
    }

    public void PlayButton()
    {
        if (!playerController.waterEnd)
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            PlayerPrefs.SetInt("NextLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            SceneManager.LoadScene("LevelScene");
        }
        else
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}
