using System.Collections;
using System.Collections.Generic;

public abstract class ShuffleList
{
    /// <summary>
    /// Method to shuffle a list using Fisher-Yates Shuffle
    /// </summary>
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> shuffledList = new List<E>(inputList); // Create a copy of the original list
        System.Random random = new System.Random();

        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            // Generate a random index between 0 and i (inclusive)
            int j = random.Next(0, i + 1);

            // Swap elements at index i and j
            E temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        return shuffledList; // Return the shuffled list
    }
}
