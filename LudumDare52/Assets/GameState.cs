using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public int Health = 100;

    public int Coins;

    public TMP_Text HealthText;

    public TMP_Text CoinsText;
    
    // Start is called before the first frame update
    void Start()
    {
        Coins = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Health = Mathf.Clamp(Health, 0, 100);
        Coins = Mathf.Clamp(Coins, 0, 1000);
        HealthText.text = Health.ToString();
        CoinsText.text = Coins.ToString();

        if (Health == 0)
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        }
    }

    public void SetHealth(int amount)
    {
        this.Health = amount;
    }

    public int GetHealth()
    {
        return this.Health;
    }
    
    public void SetCoins(int amount)
    {
        this.Coins = amount;
    }

    public int GetCoins()
    {
        return this.Coins;
    }
}
