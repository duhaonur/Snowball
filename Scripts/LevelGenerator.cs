using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levels;
    public PlayerController playerController;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            if(PlayerPrefs.GetInt("CurrentLevel") <= levels.Length)
            {
                int x = PlayerPrefs.GetInt("CurrentLevel");
                x = x - 1;
                Instantiate(levels[x], levels[x].transform.position, levels[x].transform.rotation);
            }
            else
            {
                int y = Random.Range(0, 40);
                Instantiate(levels[y], levels[y].transform.position, levels[y].transform.rotation);
            } 
        }
        if(PlayerPrefs.GetInt("CurrentLevel") > 15)
        {
            playerController.speed = 45;
            playerController.force = 115;
        }
        else if (PlayerPrefs.GetInt("CurrentLevel") > 25)
        {
            playerController.speed = 50;
            playerController.force = 120;
        }
    }
}
