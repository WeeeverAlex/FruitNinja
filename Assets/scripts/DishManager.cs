using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DishManager : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public GameObject ricePrefab;

    public Image[] dishImages; 
    public Sprite[] dishSprites; 
    public Sprite riceSprite; 
    public Sprite collectedRiceSprite; 
    public Sprite[] ingredientSprites; 
    public Sprite[] collectedIngredientSprites; 

    private List<Dish> dishes = new List<Dish>();
    private GameManager gameManager;
    
    private  bool precisa_cortar;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found.");
        }
        GenerateRandomDishes(2);
    }

    private void GenerateRandomDishes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Dish newDish = new Dish();
            int fishIndex = Random.Range(0, fishPrefabs.Length);
            string fish = fishPrefabs[fishIndex].name;

            newDish.ingredients[fish] = 1;
            newDish.ingredients[ricePrefab.name] = 1;
            newDish.dishSprite = dishSprites[fishIndex];
            newDish.ingredientSprites[fish] = ingredientSprites[fishIndex];
            newDish.collectedIngredientSprites[fish] = collectedIngredientSprites[fishIndex];
            newDish.ingredientSprites[ricePrefab.name] = riceSprite;
            newDish.collectedIngredientSprites[ricePrefab.name] = collectedRiceSprite;

            dishes.Add(newDish);
        }
        DisplayDishes();
    }

    public void IngredientSliced(string ingredient)
    {
        precisa_cortar = false;
        List<Dish> completedDishes = new List<Dish>();
        foreach (Dish dish in dishes.ToList())
        {
            if (dish.ingredients.ContainsKey(ingredient) && dish.ingredients[ingredient] > 0)
            {
                dish.ingredients[ingredient]--;
                if (!dish.ingredients.Any(kv => kv.Value > 0))
                {
                    completedDishes.Add(dish);
                }
                precisa_cortar = true;
                break;
            }
        }

        if (!precisa_cortar)
        {
            gameManager.RemoveTime(5f);
        }
        foreach (Dish completedDish in completedDishes)
        {
            dishes.Remove(completedDish);
            GenerateRandomDishes(1);
            gameManager.AddScore(100);
            gameManager.AddTime(5f);
        }

        DisplayDishes();
    }

    private void DisplayDishes()
    {
        for (int i = 0; i < dishImages.Length; i++)
        {
            if (i < dishes.Count)
            {
                Dish dish = dishes[i];
                var ingredientKeys = dish.ingredients.Keys.ToArray();

                Transform dishTransform = dishImages[i].transform;
                Image pratoImage = dishTransform.Find("Prato").GetComponent<Image>();
                Image ingrediente1Image = dishTransform.Find("Ingrediente1").GetComponent<Image>();
                Image ingrediente2Image = dishTransform.Find("Ingrediente2").GetComponent<Image>();

                pratoImage.sprite = dish.dishSprite;

                if (ingredientKeys[0] == ricePrefab.name)
                {
                    ingrediente1Image.sprite = GetIngredientSprite(dish, ingredientKeys[0]);
                    ingrediente2Image.sprite = GetIngredientSprite(dish, ingredientKeys[1]);
                }
                else
                {
                    ingrediente1Image.sprite = GetIngredientSprite(dish, ingredientKeys[1]);
                    ingrediente2Image.sprite = GetIngredientSprite(dish, ingredientKeys[0]);
                }

                dishImages[i].gameObject.SetActive(true);
            }
            else
            {
                dishImages[i].gameObject.SetActive(false);
            }
        }
    }

    private Sprite GetIngredientSprite(Dish dish, string ingredient)
    {
        if (dish.ingredients[ingredient] > 0)
        {
            return dish.ingredientSprites[ingredient];
        }
        else
        {
            return dish.collectedIngredientSprites[ingredient];
        }
    }
    
    public void CompleteAllDishes()
    {
        int completedDishesCount = dishes.Count;

        foreach (Dish dish in dishes.ToList())
        {
            foreach (var ingredient in dish.ingredients.Keys.ToList())
            {
                dish.ingredients[ingredient] = 0;
            }
            gameManager.AddScore(100);
            gameManager.AddTime(5f);
        }

        dishes.Clear();
        GenerateRandomDishes(completedDishesCount);
        DisplayDishes();
    }
}

public class Dish
{
    public Dictionary<string, int> ingredients = new Dictionary<string, int>();
    public Sprite dishSprite; 
    public Dictionary<string, Sprite> ingredientSprites = new Dictionary<string, Sprite>(); 
    public Dictionary<string, Sprite> collectedIngredientSprites = new Dictionary<string, Sprite>(); 
}
