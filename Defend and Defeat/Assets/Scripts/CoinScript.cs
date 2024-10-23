using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] int score = 100;

    public int GetPower()
    {
        return score;
    }
}
