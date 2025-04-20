using UnityEngine;

public class BreakableGlass : MonoBehaviour
{

    [SerializeField] GameObject _brokenGlass;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            //break the glass pane
            _brokenGlass.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
