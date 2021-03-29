using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayPanel;
	public bool isStoreEnabled = false;
    void Update()
    {
		if (Input.GetMouseButton(0) && !isStoreEnabled)
		{
			if (MobileInput.Instance.SwipeLeft)
			{
				howToPlayPanel.SetActive(false);
				Time.timeScale = 1.0f;
			}
			if (MobileInput.Instance.SwipeRight)
			{
				howToPlayPanel.SetActive(false);
				Time.timeScale = 1.0f;
			}

		}

	}

	public void EnableStore()
	{
		isStoreEnabled = true;
	}

	public void DisableStore()
	{
		isStoreEnabled = false;
	}
}
