using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;

    private CharacterMovement _player1Movement;
    private CharacterMovement _player2Movement;
    private PlayerState _player1State;
    private PlayerState _player2State;

    void Start()
    {
        _player1Movement = _player1.GetComponent<CharacterMovement>();
        _player2Movement = _player2.GetComponent<CharacterMovement>();
        _player1State = _player1.GetComponent<PlayerState>();
        _player2State = _player2.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (_player1State.Finished && _player2State.Finished) Win();
    }

    public void StartButton()
    {
        if (_player1Movement.IsLineFinished && _player2Movement.IsLineFinished)
        {
            _player1Movement.IsMoving = true;
            _player2Movement.IsMoving = true;
        }
    }

    public void Lose()
    {
        Time.timeScale = 0;
        _loseCanvas.SetActive(true);
    }

    public void Win()
    {
        _winCanvas.SetActive(true);
    }

    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
