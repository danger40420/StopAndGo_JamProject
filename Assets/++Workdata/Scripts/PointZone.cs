using UnityEngine;

public class PointZone : MonoBehaviour
{
    private float Score;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Combo1" && gameObject.tag == "Point1")
        {
            Score++;
        }

        if (other.tag == "Combo2" && gameObject.tag == "Point2")
        {
            Score++;
        }

        if (other.tag == "Combo3" && gameObject.tag == "Point3")
        {
            Score++;
        }
    }
}
