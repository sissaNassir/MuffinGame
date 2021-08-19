using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coinScore = 1;

    [Header("UI")]
    public Text labelCoins;
    public Text labelScore;
    public Text labelLives;
    public GameObject panelLose;

    private int numberOfCoins = 0;
    private int totalScore = 0;
    private int lives = 0;
    private Animator animator;
    private Coroutine deathCoroutine;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        AddLives(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            numberOfCoins++;
            AddScore(coinScore);
            UpdateLabelCoins();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Muffin"))
        {
            Destroy(collision.gameObject);
            AddLives(1);
        }
    }

    private void AddLives(int quantity)
    {
        lives += quantity;
        if (lives < 0)
        {
            lives = 0;
        }
        labelLives.text = "Lives: " + lives;
    }

    public void TakeDamage(GameObject damager)
    {
        numberOfCoins -= 5;

        if (numberOfCoins < 0)
        {
            AddLives(-1);
            if (lives < 1 && deathCoroutine == null)
            {
                deathCoroutine = StartCoroutine(Death());
            }
            else
            {
                numberOfCoins = 0;
            }
        }

        if (numberOfCoins > 60)
        {
            numberOfCoins -= 10;
        }


        UpdateLabelCoins();
        Destroy(damager);
    }


    public void Die()
    {
        AddLives(-1);
        if (lives < 1 && deathCoroutine == null)
        {
            deathCoroutine = StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        animator.SetTrigger("die");
        labelScore.text = "Score: " + totalScore;
        yield return new WaitForSeconds(2.5f);
        panelLose.SetActive(true);
        Time.timeScale = 0;
    }

    private void UpdateLabelCoins()
    {
        labelCoins.text = "Coins: " + numberOfCoins;
    }

    public void AddScore(int quantity)
    {
        totalScore += quantity;
    }
}
