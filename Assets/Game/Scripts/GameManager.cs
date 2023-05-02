using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public List<Platform> Platforms = new List<Platform>();

    public Player Player;
    public Dice Dice;

    public EndGameMenu EndGameMenu;
    public DiceButton DiceButton;
    public RestartButton RestartButton;
    public CoinsText CoinsText;

    public int StepsCount;

    private Quaternion _playerRotation;
    private Vector3 _playerPos;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        _playerPos = GameManager.Instance.Player.transform.position;
        _playerRotation = GameManager.Instance.Player.transform.rotation;
    }

    public void EndGame()
    {
        GameManager.Instance.EndGameMenu.gameObject.SetActive(true);
        GameManager.Instance.EndGameMenu.SetStepsText();

        GameManager.Instance.DiceButton.gameObject.SetActive(false);
        GameManager.Instance.RestartButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.Instance.Platforms.Clear();
        SceneManager.LoadScene(0);
        GameManager.Instance.RestartButton.gameObject.SetActive(false);
        GameManager.Instance.EndGameMenu.gameObject.SetActive(false);
        
        GameManager.Instance.DiceButton.gameObject.SetActive(true);
        GameManager.Instance.DiceButton.Init();

        GameManager.Instance.Player.Mover.enabled = true;
        GameManager.Instance.StepsCount = 0;

        foreach (var platform in GameManager.Instance.Platforms)
        {
            platform.IsActive = true;
        }

        GameManager.Instance.Player.transform.position = _playerPos;
        GameManager.Instance.Player.transform.rotation = _playerRotation;
    }
}
