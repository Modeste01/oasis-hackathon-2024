using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    // Declare required variables
    [SerializeField] private float speed;
    [SerializeField] private float xBound;

    private Vector3 startPosition;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move background while unpaused
        if (isPaused)
        {
            // Move the backgroudn cosntantly
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Loop the background around
            if (transform.position.x < xBound)
            {
                transform.position = startPosition;
            }
        }
    }

    // Sets the pause value
    public void PauseScrolling(bool pauseScrolling)
    {
        isPaused = pauseScrolling;
    }
}
