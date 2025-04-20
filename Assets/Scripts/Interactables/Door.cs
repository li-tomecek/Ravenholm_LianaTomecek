using UnityEngine;
public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }
}
