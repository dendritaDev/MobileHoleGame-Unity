using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    [Header(" Coins ")]
    [SerializeField] private TextMeshProUGUI menuCoinsText;

    private void Awake()
    {
        DataManager.onCoinsUpdated += UpdateCoins;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoins;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                SetMenu();
                break;
            case GameState.GAME:
                SetGame();
                break;
            case GameState.LEVELCOMPLETE:
                break;
            case GameState.GAMEOVER:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

    }

    private void UpdateCoins()
    {
        menuCoinsText.text = DataManager.instance.GetCoins().ToString();
    }
}
