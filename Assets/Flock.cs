using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    // relaciona o flocking manager para obter as variaveis
    public FlockingManager myManager;

    // Variavel de velocidade para o movimento do peixe
    float speed;

    //Variavel para retorno para o grupo
    bool tunning = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Limitar o campo de navegação
        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimitis * 2);

        //Realiza a construção do raycast
        RaycastHit hit = new RaycastHit();
        Vector3 direction = myManager.transform.position - transform.position;
        if(!b.Contains(transform.position))
        {
            tunning = true;
            //Realiza uma curva suave para o retorno da posição
            direction = myManager.transform.position - transform.position;
        }
        else if(Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            // Evita a colisão no pilar
            tunning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {
            tunning = false;
        }

        if(tunning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            //Controle de velocidade de retorno
            if(Random.Range(0, 100) < 10)
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }
            //Limitação o distanciamento entre os peixes
            if(Random.Range(0, 100) < 20)
            {
                ApplyRules();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    //Metodo para deixar o conjunto de peixes juntos / distanciamento
    void ApplyRules()
    {
        //Conjunto de peixes (lista)
        GameObject[] gos;
        gos = myManager.allFish;

        //Variaveis de posicionamento e velocidade
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        //Realiza a montagem e calculo de distancia entre peixes
        foreach (GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }

        //realiza a movimentação de acordo com a contagem do conjunto de peixes
        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
            }
        }

    }
}
