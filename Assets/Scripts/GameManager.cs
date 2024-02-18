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
    [SerializeField] private Image distanceLeftSlider;
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
    [SerializeField] private float amountOfDistanceBetweenEvents;
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
            eventText.text = string.Empty;
        }

        // Update slides to match variables
        foodSlider.fillAmount = foodReserve / maxFoodReserve;
        waterSlider.fillAmount = waterReserve / maxWaterReserve;

        // Reset if the player is currently in an event if space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isInEvent && isGameStarted)
        {
            StartCoroutine(RandomEvent(Random.Range(0, 4)));
        }
    }

    IEnumerator RandomEvent(int eventID)
    {
        isInEvent = true;
        distanceLeft -= amountOfDistanceBetweenEvents;

        if (Random.Range(0.0f, 1.0f) <= threshold)
        {
            switch (eventID)
            {
                case 0:
                    // Robber takes food

                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You are held at gunpoint and a group of bandits steal your food.";
                    }
                    else
                    {
                        eventText.text = "You are under the threat of a firearm, and a band of outlaws snatches your food provisions from you.";
                    }
                    foodReserve -= Random.Range(1, 4);
                    break;
                case 1:
                    // Robber takes water
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You are held at gunpoint and a group of bandits steal your water.";
                    }
                    else
                    {
                        eventText.text = "You are under the threat of a firearm, and a band of outlaws steals from you one a significant water reserve.";
                    }
                    waterReserve -= Random.Range(2, 5);
                    break;
                case 2:
                    // Come across oasis
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "Your friend catches and prepares a quail and everyone gets a nice meal.";
                    }
                    else
                    {
                        eventText.text = "Your companion snares and cooks a sandgrouse, allowing for everyone to have a delightful supper.";
                    }
                    foodReserve += Random.Range(1, 3);
                    break;
                case 3:
                    // Robber takes water
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "Lucky you, you come across an oasis and found some water.";
                    }
                    else
                    {
                        eventText.text = "You stumble upon a spring and accidently discover some hydration.";
                    }
                    
                    waterReserve += Random.Range(2, 4);
                    break;
                default:
                    break;
            }

            // Ensures the food and water don't go outside allowed variables
            Mathf.Clamp(waterReserve, 0, maxWaterReserve);
            Mathf.Clamp(foodReserve, 0, maxFoodReserve);
        }
        else
        {
            // Nothing happens
            switch(Random.Range(0, 5))
            {
                case 0:
                    if(Random.Range(0,2) == 0)
                    {
                        eventText.text = "You stop to take a short break.";
                    }
                    else
                    {
                        eventText.text = "You pause for a brief respite.";
                    }
                    break;
                case 1:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You pause to watch, a crow flies overheard.";
                    }
                    else
                    {
                        eventText.text = "You momentarily halt, as an eagle glides overhead.";
                    }
                    break;
                case 2:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You pause to watch, a falcon flies overheard.";
                    }
                    else
                    {
                        eventText.text = "You halt your journey, as a hawk soars in the sky above.";
                    }
                        
                    break;
                case 3:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "Your friend notices a poisonous snake in the distance.";
                    }
                    else
                    {
                        eventText.text = "In a distance, your friend notices a poisonous snake.";
                    }  
                    break;
                case 4:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You start noticing figures in the sand, it's time to take a break.";
                    }
                    else
                    {
                        eventText.text = "You begin to see shapes in the sand, it's time for a respite.";
                    }      
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
