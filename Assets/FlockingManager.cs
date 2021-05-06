using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    // Prefab para ser utilizado para instagiamento dos peixes
    public GameObject fishPrefab;

    // Numero de peixes para serem duplicados
    public int numFish = 20;

    // Lista de todos os peixes para realizar a distancia entre eles
    public GameObject[] allFish;

    //Espaçamento entre os peixes
    public Vector3 swimLimitis = new Vector3(5, 5, 5);

    //Destino
    public Vector3 goalPos;


    [Header("Configuração di Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;



    private void Start()
    {
        allFish = new GameObject[numFish];

        // Realiza o posicionamento dos peixes da lista com uma posição aleatoria dentro do range estabelecido
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimitis.x, swimLimitis.x), Random.Range(-swimLimitis.y, swimLimitis.y), Random.Range(-swimLimitis.z, swimLimitis.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }

        //Obtem a posição atual
        goalPos = this.transform.position;

    }

    private void Update()
    {
        // verifica se a distancia para não colidir
        if(Random.Range(0, 100) < 10)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimitis.x, swimLimitis.x), Random.Range(-swimLimitis.y, swimLimitis.y), Random.Range(-swimLimitis.z, swimLimitis.z));
        }
    }



}
