using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.SendMessage("Motivation boost, You are totally awesome!!!!");
    }

    void Update()
    {
        transform.Rotate(new Vector2(15, 30) * Time.deltaTime);
    }
    
}
