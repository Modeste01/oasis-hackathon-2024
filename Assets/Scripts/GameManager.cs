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
    [SerializeField] private Image foodSlider;
    [SerializeField] private Image waterSlider;
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private GameObject eventCanvas;
    [SerializeField] private GameObject dieCanvas;

    [Header("Game")]
    [SerializeField] private int foodReserve;
    [SerializeField] private int maxFoodReserve;
    [SerializeField] private int waterReserve;
    [SerializeField] private int maxWaterReserve;
    [Range(0, 1)] [SerializeField] private float threshold;
    [SerializeField] private float distanceLeft;
    [SerializeField] private float betweenEventDelay;

    private bool isGameStarted;
    private bool isInEvent;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isInEvent = false;

        introCanvas.SetActive(true);
        eventCanvas.SetActive(false);
        dieCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Start the game, if not started yet
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            isGameStarted = true;
            introCanvas.SetActive(false);

            eventCanvas.SetActive(true);
            eventText.text = "Click SPACE to start the journey.";
        }

        // Update slides to match variables
        foodSlider.fillAmount = foodReserve / maxFoodReserve;
        waterSlider.fillAmount = waterReserve / maxWaterReserve;

        // Reset if the player is currently in an event if space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isInEvent && isGameStarted)
        {
            StartCoroutine(RandomEvent(Random.Range(0, 2)));
        }
    }

    IEnumerator RandomEvent(int eventID)
    {
        isInEvent = true;

        if (Random.Range(0.0f, 1.0f) <= threshold)
        {
            switch (eventID)
            {
                case 0:
                    // Robber takes food
                    eventText.text = "You are held at gunpoint and a group of bandits steal your food.";
                    foodReserve -= Random.Range(1, 4);
                    break;
                case 1:
                    // Robber takes water
                    eventText.text = "You are held at gunpoint and a group of bandits steal your water.";
                    waterReserve -= Random.Range(2, 5);
                    break;
                default:
                    break;
            }
        }
        else
        {
            // Nothing happens
            switch(Random.Range(0, 5))
            {
                case 0:
                    eventText.text = "You stop to take a short break.";
                    break;
                case 1:
                    eventText.text = "You pause to watch, a crow flies overheard.";
                    break;
                case 2:
                    eventText.text = "You pause to watch, a falcon flies overheard.";
                    break;
                case 3:
                    eventText.text = "Your friend notices a poisonous snake in the distance.";
                    break;
                case 4:
                    eventText.text = "You start noticing figures in the sand, it's time to take a break.";
                    break;
                default:
                    eventText.text = "You notice a sandworm making its way to you.";
                    break;
            }
        }

        yield return new WaitForSeconds(betweenEventDelay);
        isInEvent = false;
    }
}
