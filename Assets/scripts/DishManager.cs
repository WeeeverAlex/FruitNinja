using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DishManager : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public GameObject ricePrefab;
    public TextMeshProUGUI[] dishDisplays;
    private List<Dish> dishes = new List<Dish>();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        GenerateRandomDishes(2); 
    }

    private void GenerateRandomDishes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Dish newDish = new Dish();
            string fish = fishPrefabs[Random.Range(0, fishPrefabs.Length)].name;
            newDish.ingredients[fish] = 1;
            newDish.ingredients[ricePrefab.name] = 1;
            dishes.Add(newDish);
        }
        DisplayDishes(); 
    }

    public void IngredientSliced(string ingredient)
    {
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
                break;
            }
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
        for (int i = 0; i < dishDisplays.Length; i++)
        {
            if (i < dishes.Count)
            {
                var ingredientsText = dishes[i].ingredients
                    .Where(kv => kv.Value > 0)
                    .Select(kv => kv.Key + " x" + kv.Value)
                    .ToArray();

                dishDisplays[i].text = string.Join(" + ", ingredientsText);
            }
            else
            {
                dishDisplays[i].text = "";
            }
        }
    }
}

public class Dish
{
    public Dictionary<string, int> ingredients = new Dictionary<string, int>();
}
