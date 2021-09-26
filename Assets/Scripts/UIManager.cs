using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Lifes and score")]
    [SerializeField] private Image[] _lifes;
    [SerializeField] private TMP_Text _score;

    [Header("Pause menu panel")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TMP_Text _controllerTitle;

    [Header("Game over panel")]
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private TMP_Text _resultScore;

    [Space]

    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        GameController.Instance.OnGameOver += ShowGameOver;
        GameController.Instance.OnGameRestart += ResetUI; 

        for (int i = 0; i < _lifes.Length; i++)
            _lifes[i].gameObject.SetActive(true);

        _score.text = GameController.Instance.PlayerScore.ToString();
    }

    public void ChangeController(ControllerType type)
    {
        _controllerTitle.text = type.ToString().ToUpper();
    }

    public void ShowGameOver()
    {
        _gameOverMenu.SetActive(true);
        _resultScore.text = GameController.Instance.PlayerScore.ToString();
    }

    public void ShowScore(int value)
    {
        _score.text = value.ToString();
    }

    public void ShowLifes()
    {
        for (int i = 0; i < _lifes.Length; i++)
            _lifes[i].gameObject.SetActive(i < GameController.Instance.PlayerLifes);
    }

    public void ResetUI()
    {
        ShowScore(GameController.Instance.PlayerScore);
        ShowLifes();
    }

    public void Exit()
    {
        GameController.Instance.GameExit();
        _pauseMenu.SetActive(false);
    }
}
