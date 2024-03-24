using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gaming;

    [SerializeField] private Text startText;

    [SerializeField] private Text racingTimeText;
    private float racingTime;
    private float finishTime;

    [SerializeField] private GameObject finishPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gaming = false;
        StartCoroutine(Starting());
    }

    private void Update()
    {
        racingTime += Time.deltaTime;
        racingTimeText.text = racingTime.ToString("F2");
    }

    IEnumerator Starting()
    {
        int timer = 3;

        startText.gameObject.SetActive(true);
        racingTimeText.gameObject.SetActive(false);

        while (timer > 0)
        {
            startText.text = timer.ToString();
            timer -= 1;
            yield return new WaitForSeconds(1f);
        }

        startText.text = "Go";
        racingTime = 0;
        racingTimeText.gameObject.SetActive(true);
        gaming = true;
        yield return new WaitForSeconds(1f);
        startText.gameObject.SetActive(false);
    }

    private void Finish()
    {

    }
}
