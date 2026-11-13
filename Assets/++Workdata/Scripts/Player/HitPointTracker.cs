using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitPointTracker : MonoBehaviour
{
    private float currentHitPoints;
    private float maxHitPoints = 4f;


    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            currentHitPoints -= 1; 
            print(currentHitPoints);
        }

        if (currentHitPoints <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    
}
