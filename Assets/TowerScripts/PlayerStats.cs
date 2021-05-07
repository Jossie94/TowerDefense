using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Currency")]
    public static int Money;

    public int startMoney = 400;

    [Header("Health")]
    public static int Lives;

    public int startLives = 20;


    public void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

}
