using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateRemoval : MonoBehaviour
{
    [SerializeField]
    public GameObject screw1;
    public GameObject screw2;
    private bool done=false;
    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        if (screw1.GetComponent<Rigidbody>().isKinematic == false
        && screw2.GetComponent<Rigidbody>().isKinematic == false && !done)
        {
            gameObject.GetComponent<Animator>().SetBool("button_pressed", true);
            sound.time = 1f;
            sound.Play();
            GameManager.Instance.UpdateGameState(RiddlesProgress.ScrewsRemoved);
            done = true;
        }
    }
}
