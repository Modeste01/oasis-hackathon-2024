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
    [SerializeField] private GameObject secondIntroCanvas;
    [SerializeField] private GameObject eventCanvas;
    [SerializeField] private GameObject dieCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject promptText;
    [SerializeField] private TMP_Text secondIntroMessage;

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
        // Reset bools
        isGameStarted = false;
        isInEvent = false;
        isGameEnded = false;

        // Reset object active state
        introCanvas.SetActive(true);
        secondIntroCanvas.SetActive(false);
        eventCanvas.SetActive(false);
        dieCanvas.SetActive(false);
        winCanvas.SetActive(false);

        // Hide camels and characters
        camels.SetActive(false);
        characters.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Show the second intro panel
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted && !isGameEnded && introCanvas.activeSelf && !secondIntroCanvas.activeSelf)
        {
            introCanvas.SetActive(false);
            secondIntroCanvas.SetActive(true);

            // Set a random max food and water
            maxFoodReserve = Random.Range(10, 15);
            maxWaterReserve = Random.Range(20, 25);
            foodReserve = maxFoodReserve;
            waterReserve = maxWaterReserve;

            // Update text to reflect random values
            secondIntroMessage.text = "Because of harsh economic and social issues, you only have access to limited supplies. You start off with <color=#B72929>" +
                maxFoodReserve + "</color> units of food and <color=#B72929>" +
                maxWaterReserve + "</color> units of water...\n\nFor many, adequate supplies isn't guaranteed, much like in this simulation.";
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted && !isGameEnded && !introCanvas.activeSelf && secondIntroCanvas.activeSelf)
        {
            // Show the camels and characters
            camels.SetActive(true);
            characters.SetActive(true);

            // Start the game and hide intro canvases
            isGameStarted = true;
            secondIntroCanvas.SetActive(false);

            // Show a blank event canvas
            eventCanvas.SetActive(true);
            eventText.text = "Press SPACE to continue walking..."; 
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isInEvent && isGameStarted && !isGameEnded && !introCanvas.activeSelf && !secondIntroCanvas.activeSelf)
        {
            StartCoroutine(RandomEvent(Random.Range(0, 4)));
        }

        // Update slides to match variables
        foodSlider.fillAmount = Mathf.Clamp((float) foodReserve / maxFoodReserve, 0, 1);
        waterSlider.fillAmount = Mathf.Clamp((float) waterReserve / maxWaterReserve, 0, 1);
    }

    IEnumerator RandomEvent(int eventID)
    {
        // Reset variables
        isInEvent = true;
        promptText.SetActive(false);

        // Traverse and pause animations and background scrolling
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

                    foodReserve -= Random.Range(3, 6);
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

                    waterReserve -= Random.Range(5, 9);
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
                        eventText.text = "You slow down to take a short break.";
                    }
                    else
                    {
                        eventText.text = "You slow down for a brief respite.";
                    }
                    break;
                case 1:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You notice a crow flying overheard.";
                    }
                    else
                    {
                        eventText.text = "You momentarily halt, as an eagle glides overhead.";
                    }
                    break;
                case 2:
                    if (Random.Range(0, 2) == 0)
                    {
                        eventText.text = "You notice a flacon flying overheard.";
                    }
                    else
                    {
                        eventText.text = "You slow your journey, as a hawk soars in the sky above.";
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

        // End game if player loses all food or water
        if (foodReserve <= 0 || waterReserve <= 0)
        {
            dieCanvas.SetActive(true);
            isGameEnded = true;
            isGameStarted = false;
        }

        // Win game is player gets to end
        if (distanceLeft <= 0 && !isGameEnded && isGameStarted)
        {
            winCanvas.SetActive(true);
            isGameEnded = true;
            isGameStarted = false;
        }

        // Wait for a few seconds before allowing player to continue
        yield return new WaitForSeconds(betweenEventDelay);

        // Traverse and unpause animations and background scrolling, if game hasn't ended
        if (!isGameEnded)
        {
            for (int i = 0; i < camelAnimators.Length; i++)
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
        }

        // Reset variables
        isInEvent = false;
        promptText.SetActive(true);
    }
}
