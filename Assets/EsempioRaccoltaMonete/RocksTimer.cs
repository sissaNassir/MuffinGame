using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocksTimer : MonoBehaviour
{
    public Image timerIcon;
    public Text number;

    private Coroutine timer;

    public void StartTimer(float stoneGenerationPause)
    {
        if(timer!=null)
        {
            StopCoroutine(timer);
        }
        timer = StartCoroutine(TimerRoutine(stoneGenerationPause));
    }

    private IEnumerator TimerRoutine(float stoneGenerationPause)
    {
        float currentTimer = stoneGenerationPause;

        UpdateUI(stoneGenerationPause, currentTimer);
        timerIcon.gameObject.SetActive(true);

        while(currentTimer>0f)
        {
            currentTimer -= 1f;
            yield return new WaitForSeconds(1f);
            UpdateUI(stoneGenerationPause,currentTimer);
        }

        timerIcon.gameObject.SetActive(false);
        timer = null;
    }

    private void UpdateUI(float startTimer,float currentTimer)
    {
        if(currentTimer<0f)
        {
            currentTimer = 0f;
        }

        number.text = "" + currentTimer;
        timerIcon.fillAmount = currentTimer / startTimer;
    }
}
