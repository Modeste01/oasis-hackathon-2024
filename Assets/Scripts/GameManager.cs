using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Declare required variables
    [Header("UI")]
    [SerializeField] private TMP_Text eventText;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private Slider waterSlider;
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
        // Update slides to match variables
        foodSlider.value = foodReserve;
        waterSlider.value = waterReserve;
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
