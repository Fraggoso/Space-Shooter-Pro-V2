using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float _speed = 3.0f;
    [SerializeField]
    private GameObject _explosion;
    private CircleCollider2D _circleCollider2D;
    private SpawnManager _spawn;
    // Start is called before the first frame update
    void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _spawn = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _speed) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosion, transform.position , Quaternion.identity);
            _circleCollider2D.enabled = false;
            Destroy(this.gameObject, 1.8f);
            _spawn.SpawnStart();
        }

    }
}



