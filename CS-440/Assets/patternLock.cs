using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patternLock : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objectsNotGrabbable;
    public GameObject[] objectsGrabbable;
    public Material matZlock;
    public Material matlock;
    public Material greenExit;
    public Material redWarn;
    public AudioSource audioSource;
    public AudioClip wallGoingDown;
    public GameObject slidingWall;
    private bool paintingPlaced = false;
    public bool fireExtinguished = false;


    private int timeWindow;

    private bool open;

    private GameObject light = null;

    void Update()
    {
        if (light != null)
        {
            if (light.GetComponent<Light>().enabled)
                gameObject.GetComponent<MeshRenderer>().material = matZlock;

            else if (!light.GetComponent<Light>().enabled)
                gameObject.GetComponent<MeshRenderer>().material = matlock;
        }

    }


    public void onCorrectPattern()
    {
        
        for (int i = 0; i < objectsGrabbable.Length; i++)
        {
            Vector3 grabbablePos = objectsNotGrabbable[i].transform.position;
            objectsNotGrabbable[i].transform.position = objectsGrabbable[i].transform.position;
            objectsGrabbable[i].transform.position = grabbablePos;
        }
        
        /*
        Vector3 grabbablePos = objectsNotGrabbable[0].transform.position;
        objectsNotGrabbable[0].transform.position = objectsGrabbable[0].transform.position;
        objectsGrabbable[0].transform.position = grabbablePos;
        */
        /*
        for (int i = 0; i < objectsGrabbable.Length; i++)
        {
            //objectsGrabbable[i].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeAll;
            objectsGrabbable[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        */
        audioSource.Play();
        GameManager.Instance.UpdateGameState(RiddlesProgress.OpenBoxSign);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "uvLight")
        {
            light = other.gameObject;
            if (other.GetComponent<Light>().enabled)
            {
                gameObject.GetComponent<MeshRenderer>().material = matZlock;
            }

        }
        if (paintingPlaced && fireExtinguished)
        {
            paintingPlaced = false;
            Vector3 finalSlidePosition = slidingWall.transform.position;
            finalSlidePosition.y = finalSlidePosition.y - 4f;
            StartCoroutine(slideWall(slidingWall, finalSlidePosition,1f));
            AudioSource.PlayClipAtPoint(wallGoingDown, slidingWall.transform.position);
            GameManager.Instance.UpdateGameState(RiddlesProgress.ExitPushed);
        }
        else if (paintingPlaced && !fireExtinguished)
        {
            StartCoroutine(redWarning());
        }
            
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "uvLight"){
            light = null;
            gameObject.GetComponent<MeshRenderer>().material = matlock;
        }
    }

    public void paintingPlacedCorrectly()
    {
        paintingPlaced = true;
        gameObject.GetComponent<MeshRenderer>().material = greenExit;
    }

    public IEnumerator slideWall(GameObject objectToMove, Vector3 end, float speed)
    {
        yield return new WaitForSeconds(1);
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(objectToMove);
    }

    public IEnumerator redWarning()
    {

        gameObject.GetComponent<MeshRenderer>().material = redWarn;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<MeshRenderer>().material = greenExit;
    }

}
