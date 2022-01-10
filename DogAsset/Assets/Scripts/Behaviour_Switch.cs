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

    //distance to trigger behaviour
    float dist;
    float friendly_distance = 3.5f; 



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

    float GetDistanceToObject(GameObject obj)
    {
        dist = Vector3.Distance(obj.transform.position, transform.position);
        return dist;
    }

    // Update is called once per frame
    void Update()
    {
        GetDistanceToObject(player);
        if(dist <= friendly_distance)
        {
            Debug.Log("friendly");
        }
        if(dist > friendly_distance)
        {
            Debug.Log("neutral");
        }
    }
}
