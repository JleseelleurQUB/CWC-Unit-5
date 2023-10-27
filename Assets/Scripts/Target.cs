using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public GameManager gameManager;
    public ParticleSystem explosionParticle;

    public int scoreValue;
    private float xSpawnRange = 4;
    private float ySpawnPos = -2;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float torqueRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

        transform.position = RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(scoreValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }

    Vector3 RandomSpawn()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
}
