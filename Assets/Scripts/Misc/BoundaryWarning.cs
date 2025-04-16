using UnityEngine;

public class BoundaryWarning : MonoBehaviour
{
    //References to the warning message and filter
    [SerializeField] private GameObject warningMsg;
    [SerializeField] private GameObject warningFilter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Display warning
            warningMsg.SetActive(true);
            warningFilter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    { 
        if (other.tag == "Player")
        {
            //Disable warning
            warningMsg.SetActive(false);
            warningFilter.SetActive(false);
        }
    }
}
