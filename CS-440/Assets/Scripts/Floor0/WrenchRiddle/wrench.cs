using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrench : MonoBehaviour
{


    private GameObject bolt;
    
    private float prevRot;
    private bool onbolt;
    private Rigidbody rb;
    public bool boltScrewed;
    public TrapDoorController trapDoor;
    [SerializeField]
    public ParticleSystem smoke;
    // Start is called before the first frame update
    void Start()
    {
    
        prevRot = transform.localRotation.eulerAngles.y;
        onbolt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(onbolt && ! boltScrewed){
            float angle = transform.rotation.eulerAngles.y - prevRot;
            rb.transform.Rotate(0,0, angle, Space.Self);
            prevRot = transform.rotation.eulerAngles.y;
            
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bolt")
        {
            bolt = other.gameObject;
            rb = other.GetComponent<Rigidbody>();
            onbolt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == bolt)
        {
            rb = null;
            onbolt = false;
        }
    }


    public void boltRotated()
    {
        boltScrewed = true;
        smoke.Play();
        smoke.GetComponent<AudioSource>().Play();
        trapDoor.setIsOpen(true);
        GameManager.Instance.UpdateGameState(RiddlesProgress.TurnOnEngine);
    }
}