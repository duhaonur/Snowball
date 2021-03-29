using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
	public Rigidbody player;

	public GameObject steam;
	public GameObject frostGO;
	public GameObject howToPlayPanel;
	public GameObject firstCam;
	private GameObject water;
	
	public ParticleSystem frostSize;
	public ParticleSystem arcadeSpark;

	public Material pinkSkyBox;
	public Material blueSkyBox;
	public CinemachineVirtualCamera changeStartCam;
	
	public EndGameScript endGameScript;
	public HowToPlay howToPlay;
	public TrailPos trailPos;

	public Text gemCountText;

	Vector3 getBigger = new Vector3(0.8f, 0.8f, 0.8f);
	Vector3 getSmaller = new Vector3(2, 2, 2);
	Vector3 getSmallerWater = new Vector3(3, 3, 3);
	Vector3 getSmallerLava = new Vector3(0.12f, 0.12f, 0.12f);
	Vector3 getSmallerEngel = new Vector3(0.16f, 0.16f, 0.16f);

	public bool endgame = false;
	public bool waterEnd = false;
	
	private bool start = true;

	public int collectedGems = 0;

	public float force = 200f;
	public float speed = 10.0f;
	private void Awake()
    {
		if (PlayerPrefs.HasKey("CurrentLevel") && PlayerPrefs.HasKey("NextLevel"))
		{
			return;
		}
		else
		{
			PlayerPrefs.SetInt("CurrentLevel", 1);
			PlayerPrefs.SetInt("NextLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
		}
	}

	private void Start()
	{
		if (PlayerPrefs.GetInt("CurrentLevel") > 5 && PlayerPrefs.GetInt("CurrentLevel") <= 10)
		{
			RenderSettings.skybox = pinkSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 10 && PlayerPrefs.GetInt("CurrentLevel") <= 15)
		{
			RenderSettings.skybox = blueSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 15 && PlayerPrefs.GetInt("CurrentLevel") <= 20)
		{
			RenderSettings.skybox = pinkSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 20 && PlayerPrefs.GetInt("CurrentLevel") <= 25)
		{
			RenderSettings.skybox = blueSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 25 && PlayerPrefs.GetInt("CurrentLevel") <= 30)
		{
			RenderSettings.skybox = pinkSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 30 && PlayerPrefs.GetInt("CurrentLevel") <= 35)
		{
			RenderSettings.skybox = blueSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 35 && PlayerPrefs.GetInt("CurrentLevel") <= 40)
		{
			RenderSettings.skybox = pinkSkyBox;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") > 40 && PlayerPrefs.GetInt("CurrentLevel") <= 45)
		{
			RenderSettings.skybox = pinkSkyBox;
		}

		gemCountText.text = PlayerPrefs.GetInt("GemCount").ToString();

		howToPlayPanel.SetActive(true);
		Time.timeScale = 0;
	}

	private void FixedUpdate()
	{
		if (endgame)
		{
			if (gameObject.transform.localScale.x >= 0.0f)
			{
				if (!waterEnd)
				{
					gameObject.transform.localScale -= getSmaller * Time.fixedDeltaTime;
					if(gameObject.transform.localScale.x <= 0.5f)
					{
						player.useGravity = false;
						player.velocity = player.velocity.normalized * 0;
					}
				}
				else
				{
					gameObject.transform.localScale -= getSmallerWater * Time.fixedDeltaTime;

				}
			}
			return;
		}
		

		player.AddForce(0f, 0f, speed * Time.deltaTime, ForceMode.VelocityChange);

		if (start)
		{
			player.AddForce(Physics.gravity * 5, ForceMode.Acceleration);
		}

		if (Input.GetMouseButton(0))
		{
			float x = Input.GetAxis("Mouse X") * Time.deltaTime;

			if (x > 0.005f)
			{
				
				player.AddForce(force * Time.deltaTime, 0f, speed * Time.deltaTime, ForceMode.VelocityChange);
			}
			if (x < -0.005f)
			{
				player.AddForce(-force * Time.deltaTime, 0f, speed * Time.deltaTime, ForceMode.VelocityChange);
			}

		}
		frostSize.startSize = transform.localScale.x;

		if (player.velocity.magnitude > speed)
		{
			player.velocity = player.velocity.normalized * speed;
		}

		if (gameObject.transform.position.y != 0 && !endgame && gameObject.transform.localScale.x < 12.0f)
		{

			gameObject.transform.localScale += getBigger * Time.fixedDeltaTime;
		}
	}

	private IEnumerator DropToWater()
	{
		transform.GetComponent<Collider>().enabled = false;
		yield return new WaitForSeconds(1.5f);
		player.velocity = player.velocity.normalized * 0;
		player.useGravity = false;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Lava")
		{
			steam.SetActive(false);
		}
		if (other.gameObject.tag == "LavaLeftRight")
		{
			steam.SetActive(false);
		}
		if (other.gameObject.tag == "Engel")
		{
			steam.SetActive(false);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Lava")
		{
			steam.SetActive(true);
			if (gameObject.transform.localScale.x > 0.7f)
				gameObject.transform.localScale -= getSmallerLava;
		}
		if (other.gameObject.tag == "LavaLeftRight")
		{
			steam.SetActive(true);
			if (gameObject.transform.localScale.x > 0.7f)
				gameObject.transform.localScale -= getSmallerLava;
		}
		if (other.gameObject.tag == "Engel")
		{
			steam.SetActive(true);
			if (gameObject.transform.localScale.x > 0.7f)
				gameObject.transform.localScale -= getSmallerEngel;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "ChangeStartPos")
		{
			changeStartCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
			firstCam.SetActive(false);
			start = false;
			trailPos.startPos = false;
		}

		if(other.gameObject.tag == "Water")
		{
			water = other.gameObject;
			waterEnd = true;
			StartCoroutine(DropToWater());
			endGameScript.GetComponent<EndGameScript>().EndGame();
			player.velocity = player.velocity.normalized * 2;
		}
		if (other.gameObject.tag == "EndGame")
		{
			endGameScript.GetComponent<EndGameScript>().EndGame();
		}
		if(other.gameObject.tag == "LittleSnowBalls")
		{
			arcadeSpark.Play();
			Destroy(other.gameObject);
			gameObject.transform.localScale += other.gameObject.transform.localScale / 4;
		}
		if(other.gameObject.tag == "PurpleGem")
		{
			Destroy(other.gameObject);
			collectedGems++;
		}
	}
}
