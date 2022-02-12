using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test003_aggressive : MonoBehaviour
{
    MovementUtils MU;
    GameObject dog;
    GameObject player_target;
    GameObject pause_target;
    GameObject agg_position;

    public enum Step
    {
        initial,
        TurnToPos,
        WalkToPos,
        LayDown,
        TurnToPlayer,
        Stop
    }

    [SerializeField] Step current_step;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("GermanShepherd_Prefab");
        MU = dog.GetComponent<MovementUtils>();
        player_target = GameObject.Find("target");
        pause_target = GameObject.Find("pause_target");
        agg_position = GameObject.Find("agg_position");
        current_step = Step.TurnToPos;
    }

    /* 1. drehen
     * 2. wenn auf target gucken stehen
     * 3. laufen zum target
     * 4. wenn da, stehen
     * */

    // Update is called once per frame
    void Update()
    {
        switch (current_step)
        {
            case Step.TurnToPos:
                /* 
                 * 1. drehen
                 * 2. wenn auf target gucken stehen
                 */
                bool are_we_facing_the_agg_target = MU.turn_until_facing(agg_position, true);

                if (are_we_facing_the_agg_target)
                    current_step = Step.WalkToPos;
                break;
            case Step.WalkToPos:
                /*
                 * 3. laufen zum target = pause location
                 */
                bool are_we_touching_the_agg_pos = MU.walk_until_touching(agg_position, 3f);

                if (are_we_touching_the_agg_pos)
                    current_step = Step.TurnToPlayer;
                break;
            case Step.TurnToPlayer:
                //drehen Sie sich bitte zum Player um!
                bool are_we_facing_the_player = MU.turn_until_facing(player_target);

                if (are_we_facing_the_player)
                    current_step = Step.Stop;
                break;
            case Step.Stop:
                /*
                 * 4. Do nothing
                 */
                break;
            default:
                break;
        }
    }
}
