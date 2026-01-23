using UnityEngine;

public class PlayerParanter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GroundCheck")
        {
            collision.transform.parent.SetParent(transform);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundCheck")
        {
            collision.transform.parent.SetParent(null);
        }
    }
}
