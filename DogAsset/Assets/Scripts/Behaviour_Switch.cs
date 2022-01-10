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

    //behaviour states
    enum Behaviour_state
    {
        friendly,
        neutral,
        agressive
    }
    Behaviour_state dog_behaviour;
    Behaviour_state current_behaviour;
    Behaviour_state new_behaviour;


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

        if (dist <= friendly_distance)
        {
            current_behaviour = Behaviour_state.friendly;
        }
        else current_behaviour = Behaviour_state.neutral;

        dog_behaviour = current_behaviour;

    }

    float GetDistanceToObject(GameObject obj)
    {
        dist = Vector3.Distance(obj.transform.position, transform.position);
        return dist;
    }

    // Update is called once per frame
    void Update()
    {
        //ruft wohl jeden frame switchscripts auf 
        //neutral scrioot kann nicht dinge tun, weil an aus an aus
        GetDistanceToObject(player);
        if(dist <= friendly_distance)
        {
            new_behaviour = Behaviour_state.friendly;
            SwitchScripts();
            Debug.Log("friendly");
        }
        if(dist > friendly_distance)
        {
            new_behaviour = Behaviour_state.neutral;
            SwitchScripts();
            Debug.Log("neutral");
        }
    }

    void SwitchScripts()
    {
        friendly_script.enabled = false;
        agressive_script.enabled = false;
        neutral_script.enabled = false;

        if (dog_behaviour == Behaviour_state.friendly)
        {
            friendly_script.enabled = true;
        }
        if (dog_behaviour == Behaviour_state.neutral)
        {
            neutral_script.enabled = true;
        }
        if (dog_behaviour == Behaviour_state.agressive)
        {
            agressive_script.enabled = true;
        }
    }
}
