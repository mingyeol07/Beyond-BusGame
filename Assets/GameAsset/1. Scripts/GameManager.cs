using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Slider slider_gauge;
    private float curGauge;

    [SerializeField] private GameObject pnl_clear;
    public List<BusGuest> busGuests = new List<BusGuest>();

    private Transform busTransform;

    private void Start()
    {
        busTransform = GameObject.FindGameObjectWithTag("Bus").transform;
        StartCoroutine(GuestCheck());
    }

    private IEnumerator GuestCheck()
    {
        while(true)
        {
            float add = 0;

            for(int i =0; i< busGuests.Count; i++)
            {
                if(busGuests[i].BusGuestType == BusGuestType.NuisanceGuest)
                {
                    add -= 0.05f;
                }
                else
                {
                    add += 0.025f;
                }

                busGuests[i].transform.rotation = busTransform.rotation;
            }
            curGauge += add;

            if (curGauge < 0) curGauge = 0;

            slider_gauge.value = curGauge / 1;
            if(curGauge >= 1)
            {
                GameClear();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void GameClear()
    {
        pnl_clear.SetActive(true);
    }
}
