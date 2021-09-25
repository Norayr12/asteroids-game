using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Lifes and score")]
    [SerializeField] private Image[] _lifes;
    [SerializeField] private TMP_Text _score;

    [Header("Pause menu panel")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _resumeButton, _restartButton, _exitButton;

    [Space]

    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _pauseButton.onClick.AddListener( () =>
        {
            GameController.Instance.Pause();
            _pauseMenu.SetActive(true);
        });

        _resumeButton.onClick.AddListener( () => 
         {
            GameController.Instance.Resume();
            _pauseMenu.SetActive(false);
         });

        _restartButton.onClick.AddListener(() =>
        {
            GameController.Instance.Restart();
            _pauseMenu.SetActive(false);
        });

        _exitButton.onClick.AddListener(() =>
        {
            GameController.Instance.GameExit();
            _pauseMenu.SetActive(false);
        });

        for (int i = 0; i < _lifes.Length; i++)
            _lifes[i].gameObject.SetActive(true);

        _score.text = 0.ToString();
    }

    public void ShowScore(int value)
    {
        _score.text = value.ToString();
    }
}
