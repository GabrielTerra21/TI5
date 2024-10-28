using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleControl : MonoBehaviour
{
    public int[] result, correctCombination;
    private void Start()
    {
        result = new int[]{5,5,5};
        correctCombination = new int[]{4,2,7};
        PuzzleRotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel1":
                result[0] = number;
                break;
            case "wheel2":
                result[1] = number;
                break;
            case "wheel3":
                result[2] = number;
                break;
        }
        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            Debug.Log("Opened!");
            // fazer com que destrave a porta
        }
    }

    private void OnDestroy()
    {
        PuzzleRotate.Rotated -= CheckResults;
    }
}
