using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Switch : MonoBehaviour
{
    //get scripts
    GameObject dog;
    Friendly_Behaviour friendly_script;
    Neutral_Behaviour neutral_script;
    Aggressive_Behaviour agressive_script;

    //get Player
    GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        //get scripts 
        dog = GameObject.Find("GermanShepherd_Prefab");
        friendly_script = dog.GetComponent<Friendly_Behaviour>();
        neutral_script = dog.GetComponent<Neutral_Behaviour>();
        agressive_script = dog.GetComponent<Aggressive_Behaviour>();

        friendly_script.enabled = false;
        agressive_script.enabled = false;
        neutral_script.enabled = false;

        //get player
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log("Distance is. " + dist);
        }
    }
}
