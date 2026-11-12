using Unity.VisualScripting;
using UnityEngine;

public class HitPointTracker : MonoBehaviour
{
    private float currentHitPoints;
    private float maxHitPoints;
    
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            currentHitPoints -= 1; 
        }
    }
    
    
}
