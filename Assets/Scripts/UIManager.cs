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
        GameController.Instance.OnUpdate += OnUpdate;
        GameController.Instance.OnGameOver += OnGameOver;
        GameController.Instance.OnGameRestart += OnGameRestart; 

        for (int i = 0; i < _lifes.Length; i++)
            _lifes[i].gameObject.SetActive(true);

        _score.text = GameController.Instance.PlayerScore.ToString();
    }

    public void ChangeController(ControllerType type)
    {
        _controllerTitle.text = type.ToString().ToUpper();
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

    private void OnGameOver()
    {
        _gameOverMenu.SetActive(true);
        _resultScore.text = GameController.Instance.PlayerScore.ToString();
    }

    private void OnUpdate()
    {
        if(Time.timeScale != 0 && Input.GetKeyDown(GameData.UIConfig.PauseButton))
        {
            GameController.Instance.Pause();
            _pauseMenu.SetActive(true);
        }
    }

    private void OnGameRestart()
    {
        ShowScore(GameController.Instance.PlayerScore);
        ShowLifes();
    }
}
