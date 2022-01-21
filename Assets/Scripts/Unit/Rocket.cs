using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//le script rocket devait servir a faire tomber des rocket sur la grid prés des astronautes de facon random 
//en placant un avertissement 1 tour avant et pour arrêter leur chute détruire un radar présent sur la grid
//mais pas fait faute de temps
public class Rocket : MonoBehaviour
{
    Vector3 target = new Vector3(15, 100, 10);
    float speed = 5.5f;

    Vector3 posA = new Vector3();
    Vector3 posB = new Vector3();
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
            Destroy(gameObject);

        if (posA != null && posA != transform.position)
            posB = posA;
        posA = transform.position;

        Vector3 dir = (posB - posA).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        this.gameObject.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
    }
}
