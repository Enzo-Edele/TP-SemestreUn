using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailActivator : MonoBehaviour
{
    [SerializeField]List<GameObject> wagon;
    int currentWagon = 0;
    private void Start()
    {
        if (currentWagon < wagon.Count)
        {
            wagon[currentWagon].SetActive(true);
            currentWagon++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(currentWagon < wagon.Count)
        {
            wagon[currentWagon].SetActive(true);
            currentWagon++;
        }
    }
    //why not faire un truc qui génére des rames de monorail random
}
