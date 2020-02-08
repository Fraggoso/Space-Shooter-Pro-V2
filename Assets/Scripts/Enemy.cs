using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _anim;
    [SerializeField]
    private AudioClip _explosionSound;
    private AudioSource _audioSource;
    private float _canFire = -1.0f;
    private float _fireRate = 0.15f;
    private bool _canLaserShoot = true;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _explosionSound;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
        if (_canLaserShoot == true && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player>().DamageCalculation();
            PlaySound();
            EnemyDeath();
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            PlaySound();
            EnemyDeath();
        }
    }
    private void EnemyDeath()
    {
        _canLaserShoot = false;
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 5.0f);
        _anim.SetTrigger("OnEnemyDeath");
        _player.ScoreCalculation(10);
        _speed = 1.5f;
    }
    void PlaySound()
    {
        _audioSource.Play();
    }

    private void FireLaser()
    {
        _fireRate = Random.Range(3f, 7f);
        _canFire = Time.time + _fireRate;

        GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();
        }
    }
}
