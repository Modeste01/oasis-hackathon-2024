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
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject promptText;

    [Header("Characters")]
    [SerializeField] private GameObject camels;
    [SerializeField] private GameObject characters;
    [SerializeField] private Animator[] camelAnimators;
    [SerializeField] private Animator[] characterAnimators;
    [SerializeField] private ParallaxManager[] parallaxBackgrounds;

    [Header("Game")]
    [SerializeField] private int foodReserve;
    [SerializeField] private int maxFoodReserve;
    [SerializeField] private int waterReserve;
    [SerializeField] private int maxWaterReserve;
    [Range(0, 1)] [SerializeField] private float threshold;
    [SerializeField] private float distanceLeft;
    [SerializeField] private float maxDistance;
    [SerializeField] private float amountOfDistanceBetweenEvents;
    [SerializeField] private float betweenEventDelay;

    private bool isGameStarted;
    private bool isInEvent;
    private bool isGameEnded;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isInEvent = false;
        isGameEnded = false;

        introCanvas.SetActive(true);
        eventCanvas.SetActive(false);
        dieCanvas.SetActive(false);
        winCanvas.SetActive(false);

        camels.SetActive(false);
        characters.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Start the game, if not started yet
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted && !isGameEnded)
        {
            camels.SetActive(true);
            characters.SetActive(true);

            isGameStarted = true;
            introCanvas.SetActive(false);

            eventCanvas.SetActive(true);
            eventText.text = string.Empty;
        }

        // Update slides to match variables
        foodSlider.fillAmount = foodReserve / maxFoodReserve;
        waterSlider.fillAmount = waterReserve / maxWaterReserve;

        // Reset if the player is currently in an event if space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isInEvent && isGameStarted && !isGameEnded)
        {
            StartCoroutine(RandomEvent(Random.Range(0, 4)));
        }
    }

    IEnumerator RandomEvent(int eventID)
    {
        // Reset variables
        isInEvent = true;
        promptText.SetActive(false);

        // Traverse and pause animations and background scrolling
        for(int i = 0; i < camelAnimators.Length; i++)
        {
            camelAnimators[i].enabled = false;
        }

        for (int i = 0; i < characterAnimators.Length; i++)
        {
            characterAnimators[i].enabled = false;
        }

        for (int i = 0; i < camelAnimators.Length; i++)
        {
            parallaxBackgrounds[i].PauseScrolling(true);
        }

        // Decide if an event happens
        if (Random.Range(0.0f, 1.0f) <= threshold)
        {
            // Play an event based on a random number
            switch (eventID)
            {
                case 0:
                    // Lose food
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
                    // Lose water
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "A camel gets spooked and spills some water...";
                    }
                    else
                    {
                        eventText.text = "You are under the threat of a firearm, and a band of outlaws steals from you one a significant water reserve.";
                    }

                    waterReserve -= Random.Range(2, 5);
                    break;
                case 2:
                    // Getting food
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
                    // Getting water
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

        // Adjust distance value and slider fill
        distanceLeft -= amountOfDistanceBetweenEvents;
        distanceLeftSlider.fillAmount += amountOfDistanceBetweenEvents / maxDistance;

        // Wait for a few seconds before allowing player to continue
        yield return new WaitForSeconds(betweenEventDelay);

        // Reset variables
        isInEvent = false;
        promptText.SetActive(true);

        // End game if player loses all food or water
        if (foodReserve <= 0 || waterReserve <= 0)
        {
            dieCanvas.SetActive(true);
            isGameEnded = true;
        }

        // Win game is player gets to end
        if (distanceLeft < 0 && !isGameEnded)
        {
            winCanvas.SetActive(true);
            isGameEnded = true;
        }

        // Traverse and unpause animations and background scrolling, if game hasn't ended
        if (!isGameEnded)
        {
            for (int i = 0; i < camelAnimators.Length; i++)
            {
                camelAnimators[i].enabled = true;
            }

            for (int i = 0; i < characterAnimators.Length; i++)
            {
                characterAnimators[i].enabled = true;
            }

            for (int i = 0; i < camelAnimators.Length; i++)
            {
                parallaxBackgrounds[i].PauseScrolling(false);
            }
        }
    }
}
