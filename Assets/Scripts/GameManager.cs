using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Declare required variables
    [Header("Objects")]
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private GameObject eventCanvas;
    [SerializeField] private GameObject dieCanvas;

    [Header("Game")]
    [SerializeField] private int foodReserve;
    [SerializeField] private int waterReserve;
    [Range(0, 1)] [SerializeField] private float threshold;
    [SerializeField] private float distanceLeft;

    private float randomNumber;
    private int eventId;

    // Start is called before the first frame update
    void Start()
    {
        threshold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomEvent()
    {
        switch(eventId)
        {
            case 0:
                // Robber takes food

                break;
            case 1:
                // Robber takes water

                break;
            default:
                break;
        }
    }
}
