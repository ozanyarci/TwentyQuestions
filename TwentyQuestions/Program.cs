using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static string animalType = ""; // Declare animalType at a higher scope

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to 20 Questions!");

        while (true)
        {
            // Prompt the player to choose a category
            Console.WriteLine("Choose a category by entering the corresponding number:");
            Console.WriteLine("1. Animal  2. Object  3. Place  4. Exit");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the game. Goodbye!");
                return;
            }

            int categoryChoice;
            if (!int.TryParse(input, out categoryChoice) || categoryChoice < 1 || categoryChoice > 4)
            {
                Console.WriteLine("Invalid choice. Please choose 1, 2, 3, or 4.");
                continue;
            }

            // Initialize variables for the secret object and its category
            string secretObject = "";
            string category = "";

            switch (categoryChoice)
            {
                case 1: // Animal
                    category = "animal";
                    animalType = ChooseAnimalType(); // Assign the selected animal type to animalType
                    secretObject = GetRandomAnimal(animalType);
                    break;
                case 2: // Object
                    category = "object";
                    secretObject = GetRandomObject();
                    break;
                case 3: // Place
                    category = "place";
                    secretObject = GetRandomPlace();
                    break;
                case 4: // Exit
                    Console.WriteLine("Exiting the game. Goodbye!");
                    return;
            }

            Console.WriteLine($"You selected {category}. Let's begin!");

            // Display all options in the chosen category
            DisplayOptions(category);

            // Number of questions allowed
            int questionsLeft = 20;

            // Track guessed items
            HashSet<string> guessedItems = new HashSet<string>();

            // Loop until the player guesses the object or runs out of questions
            while (questionsLeft > 0)
            {
                Console.WriteLine($"You have {questionsLeft} guesses left. Make your guess:");

                // Read player's guess from the console
                string guess = Console.ReadLine().ToLower();

                // Check if the guess is correct
                if (guess == secretObject)
                {
                    Console.WriteLine("Congratulations! You guessed it right.");
                    break;
                }
                else
                {
                    // Decrement the guess counter
                    questionsLeft--;

                    // Check if the guessed animal matches the selected type
                    if (category == "animal")
                    {
                        switch (animalType)
                        {
                            case "mammal":
                                if (!GetMammals().Contains(guess))
                                {
                                    Console.WriteLine("This is not a mammal from the list. Try again!");
                                    continue;
                                }
                                break;
                            case "fish":
                                if (!GetFish().Contains(guess))
                                {
                                    Console.WriteLine("This is not a fish from the list. Try again!");
                                    continue;
                                }
                                break;
                            case "bird":
                                if (!GetBirds().Contains(guess))
                                {
                                    Console.WriteLine("This is not a bird from the list. Try again!");
                                    continue;
                                }
                                break;
                        }
                    }

                    Console.WriteLine("Incorrect guess.");
                }
            }

            // If the player runs out of questions
            if (questionsLeft == 0)
            {
                Console.WriteLine("Sorry, you ran out of guesses. Game over!");
                Console.WriteLine($"The secret {category} was {secretObject}.");
            }
        }
    }

    // Method to choose the type of animal (mammal, fish, bird)
    static string ChooseAnimalType()
    {
        Console.WriteLine("Choose the type of animal:");
        Console.WriteLine("1. Mammal  2. Fish  3. Bird");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int animalTypeChoice) || animalTypeChoice < 1 || animalTypeChoice > 3)
        {
            Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
            return ChooseAnimalType();
        }

        switch (animalTypeChoice)
        {
            case 1:
                return "mammal";
            case 2:
                return "fish";
            case 3:
                return "bird";
            default:
                return ChooseAnimalType();
        }
    }

    // Method to get a random animal of a specific type
    static string GetRandomAnimal(string animalType)
    {
        List<string> animals = new List<string>();

        switch (animalType)
        {
            case "mammal":
                animals.AddRange(GetMammals());
                break;
            case "fish":
                animals.AddRange(GetFish());
                break;
            case "bird":
                animals.AddRange(GetBirds());
                break;
            default:
                break;
        }

        Random rand = new Random();
        animals = animals.OrderBy(a => rand.Next()).ToList(); // Shuffle the list
        return animals.First();
    }

    // Method to display all options in a category
    static void DisplayOptions(string category)
    {
        Console.WriteLine($"Here are the possible {category}:");
        switch (category)
        {
            case "animal":
                List<string> allAnimals = new List<string>();
                allAnimals.AddRange(GetMammals());
                allAnimals.AddRange(GetFish());
                allAnimals.AddRange(GetBirds());
                Random rand = new Random();
                allAnimals = allAnimals.OrderBy(a => rand.Next()).ToList(); // Shuffle the list
                foreach (var animal in allAnimals)
                {
                    Console.WriteLine(animal);
                }
                break;
            default:
                foreach (string option in GetOptions(category))
                {
                    Console.WriteLine(option);
                }
                break;
        }
        Console.WriteLine();
    }

    // Method to get mammals
    static string[] GetMammals()
    {
        return new string[] { "elephant", "tiger", "lion", "giraffe", "zebra", "monkey", "bear", "dog", "cat", "horse", "rabbit", "fox", "wolf", "deer", "buffalo", "hippo", "rhino", "dolphin", "whale" }; // Include dolphins and whales
    }

    // Method to get fish
    static string[] GetFish()
    {
        return new string[] { "shark", "octopus", "jellyfish", "starfish", "seahorse", "crab", "lobster" }; // Remove dolphins and whales and add penguin to birds
    }

    // Method to get birds
    static string[] GetBirds()
    {
        return new string[] { "eagle", "owl", "hawk", "peacock", "swan", "parrot", "pigeon", "seagull", "vulture", "penguin" }; // Move penguin to birds
    }

    // Method to get objects or places
    static string[] GetOptions(string category)
    {
        switch (category)
        {
            case "object":
                return new string[] {
                    "chair", "table", "book", "phone", "computer", "television", "bicycle", "lamp", "umbrella", "glasses",
                    "pen", "notebook", "wallet", "backpack", "camera", "guitar", "keyboard", "clock", "mirror", "painting"
                };
            case "place":
                return new string[] {
                    "forest", "desert", "beach", "island",
                    "house", "building", "school", "hospital", "restaurant", "park", "zoo", "museum", "library", "theater"
                };
            default:
                return null;
        }
    }

    // Method to get a random object
    static string GetRandomObject()
    {
        string[] objects = GetOptions("object");
        Random rand = new Random();
        return objects[rand.Next(objects.Length)];
    }

    // Method to get a random place
    static string GetRandomPlace()
    {
        string[] places = GetOptions("place");
        Random rand = new Random();
        return places[rand.Next(places.Length)];
    }
}
