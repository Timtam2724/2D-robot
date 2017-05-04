using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform[] teleport;
    public GameObject[] prefab;
	
	
    void Start()
    { //this will spawn only one prefeb, if you want call it many time, create  a new function and call it or create for loop
        int tele_num = Random.Range(0, teleport.Length);
        int prefeb_num = Random.Range(0, prefab.Length);

        Instantiate(prefab[prefeb_num], teleport[tele_num].position, teleport[tele_num].rotation);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
