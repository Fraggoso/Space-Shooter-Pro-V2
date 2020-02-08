using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private float _fireRate = 0.75f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _multiplySpeed = 2f;
    [SerializeField]
    private GameObject _spriteShield;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private bool _tripleShotEnabled = false;
    [SerializeField]
    private bool _shieldEnabled = false;
    [SerializeField]
    private bool _speedEnabled = false;
    private UIManager _canvasUIManager;
    private Transform _rightEngine, _leftEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;




    private SpawnManager _spawnmanager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _canvasUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _rightEngine = transform.Find("Right_Engine");
        _leftEngine = transform.Find("Left_Engine");
        _audioSource = gameObject.GetComponent<AudioSource>();

        _audioSource.clip = _laserSoundClip;
    }


    void Update()
    {

        CalculateMovement();
        
        if (Input.GetMouseButtonDown(0) && Time.time > _canFire)
        {
            FireLaser();
        }

    }



    void CalculateMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalAxis, verticalAxis, 0) * _speed * _multiplySpeed * Time.deltaTime);

        if (transform.position.x > 11.0f)
        {
            transform.position = new Vector3(-11.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.0f)
        {
            transform.position = new Vector3(11.0f, transform.position.y, 0);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }
    }
    
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShotEnabled == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
         }
        else if (_tripleShotEnabled == true)
        {
            Instantiate(_tripleShotPowerupPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            StartCoroutine(TripleShotPowerupCoolDown());
        }
        _audioSource.Play();
    }

    IEnumerator TripleShotPowerupCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotEnabled = false;
    }

    public void TripleShotPowerupEnable()
    {
        if (_tripleShotEnabled == true)
        {
            StartCoroutine(TripleShotPowerupCoolDown());
        }
        else
        {
            _tripleShotEnabled = true;
        }
    }

    public void SpeedPowerupEnabled()
    {
        if (_speedEnabled == true)
        {
            return;
        }
        _speedEnabled = true;
        _multiplySpeed = 2.0f;
        StartCoroutine(SpeedPowerupCoolDown());
    }

    IEnumerator SpeedPowerupCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speedEnabled = false;
        _multiplySpeed = 1.0f;
    }

    public void ShieldPowerupEnabled()
    {
        _shieldEnabled = true;
        _spriteShield.SetActive(true);
    }

    public void DamageCalculation()
    {
        if (_shieldEnabled == true)
        {
            _shieldEnabled = false;
            _spriteShield.SetActive(false);
            return;
        }
        _lives--;
        _canvasUIManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _rightEngine.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.gameObject.SetActive(true);    
        }

        if (_lives < 1)
        {
            Debug.Log("Player destroyed");
            _spawnmanager.DeactivateSpawn();
            Destroy(this.gameObject);

        }
    }

    public void ScoreCalculation(int points)
    {
        _score += points;
        _canvasUIManager.PointsCalculation(_score);
    }

}
