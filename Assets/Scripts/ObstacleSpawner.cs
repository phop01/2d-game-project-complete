using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] obstacles;
    public bool isGameOver;
    public static ObstacleSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartCoroutine("SpawObstacle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateObstacle()
    {
        int random = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[random], transform.position, Quaternion.identity);
    }
    IEnumerator SpawObstacle()
    {
        while (!isGameOver)
        {
            CreateObstacle();
            yield return new WaitForSeconds(2F);
        }
    }
}