using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Text startText;
    bool gaming;

    [SerializeField] private Text timeText;
    private float time;
    private float finishTime;

    [SerializeField] private GameObject finishPanel;
   

    private void Start()
    {
        StartCoroutine(Starting());
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("F2");
    }


    IEnumerator Starting()
    {
        startText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(false);
        startText.text = "3";
        yield return new WaitForSeconds(1f);
        startText.text = "2";
        yield return new WaitForSeconds(1f);
        startText.text = "1";
        yield return new WaitForSeconds(1f);
        startText.text = "Go";
        time = 0;
        timeText.gameObject.SetActive(true);
        gaming = true;
        yield return new WaitForSeconds(1f);
        startText.gameObject.SetActive( false);
    }

    private void Finish()
    {

    }
}
