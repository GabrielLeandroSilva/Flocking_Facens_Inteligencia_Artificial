using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    // relaciona o flocking manager para obter as variaveis
    public FlockingManager myManager;

    // Variavel de velocidade para o movimento do peixe
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
