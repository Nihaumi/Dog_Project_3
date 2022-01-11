using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_Behaviour : MonoBehaviour
{
    //ENUM WAS HERE
    public enum Animation_state //is a class
    {
        sitting,
        standing,
        walking,
        running,
        lying,
        sleeping,
        aggressiv,
        turning
    };

    public Animation_state dog_state;

    //timer
    public float change_anim_timer;
    float starting_timer = 5f;
    int new_timer;
    int min_timer;
    int max_timer;
    int turning_timer = 1;

    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;
    Basic_Behaviour basic_behav;

    private void Awake()
    {
        change_anim_timer = starting_timer;
    }

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();

        //state
        anim_controll.current_state = anim.stand_01;
        dog_state = Animation_state.standing;

        //timer
        if(dog_state == Animation_state.turning)
        {
            change_anim_timer = turning_timer;
        }
        min_timer = 5;
        max_timer = 10;
    }

    //Timer
    void ResetTimerFunction()
    {
        if (change_anim_timer <= 0)
        {
            change_anim_timer = ChooseRandomTimer();
            SetLongTimer();
        }
    }
    int ChooseRandomTimer()
    {
        new_timer = Random.Range(min_timer, max_timer);
        return new_timer;
    }

    void SetShortTimer()
    {
        min_timer = 2;
        max_timer = 3;
    }

    void SetLongTimer()
    {
        min_timer = 5;
        max_timer = 10;
    }

    //choosing random index of animation lists
    int random_index = 0;
    int list_length = 0;
    int ChooseRandomIndex()
    {
        switch (dog_state)
        {
            case Animation_state.turning:
                break;
            case Animation_state.standing:
                GetRandomIndexFromList(anim.list_standing);
                DisplayList(anim.list_standing);
                break;
            case Animation_state.sitting:
                GetRandomIndexFromList(anim.list_sitting); ;
                DisplayList(anim.list_sitting);
                break;
            case Animation_state.sleeping:
                GetRandomIndexFromList(anim.list_sleeping);
                DisplayList(anim.list_sleeping);
                break;
            case Animation_state.walking:
                GetRandomIndexFromList(anim.list_walking);
                DisplayList(anim.list_walking);
                break;
            case Animation_state.running:
                GetRandomIndexFromList(anim.list_running);
                DisplayList(anim.list_running);
                break;
            case Animation_state.lying:
                GetRandomIndexFromList(anim.list_lying);
                DisplayList(anim.list_lying);
                break;

            default:
                break;
        }
        //Debug.Log("index " + random_index);

        return random_index;
    }
    public void GetRandomIndexFromList(List<string> list)
    {
        random_index = Random.Range(0, list.Count - 1);
    }

    void DisplayList(List<string> list)
    {
        list_length = list.Count;
        int i = 0;
        while (i < list_length)
        {
            //Debug.Log("list item number: "+ i + "is" + list[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        change_anim_timer = change_anim_timer - Time.deltaTime;

        if (change_anim_timer <= 0)
        {
            ChooseRandomIndex();
            switch (dog_state)
            {
                case Animation_state.turning:
                    Debug.Log("turning state reached");
                    break;
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(anim.list_standing[random_index]);
                    Debug.Log("standinglist item at rndindex: " + random_index + "is:" + anim.list_standing[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.sitting;
                    }
                    if (random_index > 1)
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.sitting:
                    anim_controll.ChangeAnimationState(anim.list_sitting[random_index]);
                    Debug.Log("sitting list item at rndindex: " + random_index + "is:" + anim.list_sitting[random_index]);
                    dog_state = Animation_state.walking;
                    break;
                case Animation_state.lying:
                    anim_controll.ChangeAnimationState(anim.list_lying[random_index]);
                    Debug.Log("lying list item at rndindex: " + random_index + "is:" + anim.list_lying[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.sleeping;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.sleeping:
                    anim_controll.ChangeAnimationState(anim.list_sleeping[random_index]);
                    Debug.Log("sleeping list item at rndindex: " + random_index + "is:" + anim.list_sleeping[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.standing;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(anim.list_walking[random_index]);
                    Debug.Log("walking list item at rndindex: " + random_index + "is:" + anim.list_walking[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.standing;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.running;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                case Animation_state.running:
                    anim_controll.ChangeAnimationState(anim.list_running[random_index]);
                    Debug.Log("running list item at rndindex: " + random_index + "is:" + anim.list_running[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.standing;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                    }
                    break;
                default:
                    return;
            }
            ResetTimerFunction();
            Debug.Log("new state " + dog_state);
        }
    }
}
