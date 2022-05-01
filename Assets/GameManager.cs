using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3;
    public float respawnInvulnerabilityTime = 3f;
    public int score = 0;
    public Image scoreImage;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if(asteroid.size < 1.25f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
        scoreText.text = score.ToString();
        

        
    }
    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        scoreImage.fillAmount = (float)lives/3;
        if(this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);

        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);

    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        scoreImage.fillAmount = lives/3;
        this.score = 0;
        scoreText.text = score.ToString();
        Invoke(nameof(Respawn), respawnTime);

    }
}
