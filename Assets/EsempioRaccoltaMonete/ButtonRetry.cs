using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRetry : MonoBehaviour {
    Button button;

	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(RestartGame);
	}

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
