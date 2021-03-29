using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelProgress : MonoBehaviour
{
    public Slider progressBar;

    public PlayerController PlayerController;

    public Text currentLevelText;
    public Text nextLevelText;

    private void Start()
    {
        PlayerController.GetComponent<PlayerController>();
        currentLevelText.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
        nextLevelText.text = PlayerPrefs.GetInt("NextLevel").ToString();
    }

    private void Update()
    {
        progressBar.value = PlayerController.transform.position.z;
    }
}
