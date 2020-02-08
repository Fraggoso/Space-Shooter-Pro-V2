using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerupID;
    private Player _player;

    [SerializeField]
    private AudioClip _pickUpSound;
    private AudioSource _audioSource;
    private SpriteRenderer _sprite;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource.clip = _pickUpSound;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.5f )
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (_powerupID)
            {
                case 0:
                    _player.TripleShotPowerupEnable();
                    //other.transform.GetComponent<Player>().TripleShotPowerupEnable();
                    break;

                case 1:
                    _player.SpeedPowerupEnabled();
                    //other.transform.GetComponent<Player>().SpeedPowerupEnabled();
                    break;

                case 2:
                    _player.ShieldPowerupEnabled();
                    //other.transform.GetComponent<Player>().ShieldPowerupEnabled();
                    break;

                default:
                    Debug.Log("Nichts.");
                    break;
            }
            PlaySound();
            DeactivateSpriteandBox();

            Destroy(this.gameObject, 1f);
        }
        /*else if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            _player.ScoreCalculation(300);
            Destroy(other.gameObject);
        }*/
    }
    private void PlaySound()
    {
        _audioSource.Play();
    }
    private void DeactivateSpriteandBox()
    {
        _sprite.enabled = false;
        Destroy(GetComponent<Collider2D>());
    }
}
