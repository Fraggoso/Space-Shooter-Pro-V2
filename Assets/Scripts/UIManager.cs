using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;
    private GameManager _gameManager;




    // Start is called before the first frame update
    void Start()
    {

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PointsCalculation(int _score)
    {
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
