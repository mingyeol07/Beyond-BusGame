using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TMP_Text txt_money;
    private int curMoney;

    [SerializeField] private GameObject pnl_clear;

    private void Awake()
    {
        Instance = this;
    }

    public void GuestOnBus()
    {
        curMoney += 1000;
        txt_money.text= curMoney.ToString();
    }

    public void GameClear()
    {
        pnl_clear.SetActive(true);
    }
}
