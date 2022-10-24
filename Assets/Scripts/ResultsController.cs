using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    public static ResultsController Current;

    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
        }
    }
    private void Start()
    {
        GameController.Current.delegateResetGame += DisablePanel;
    }
    public GameObject ResultsPanel;
    public Text ResultsText;

    public void WriteText(string text)
    {
        EnablePanel();
        ResultsText.text = text;
    }
    public void EnablePanel()
    {
        ResultsPanel.SetActive(true);
    }
    public void DisablePanel()
    {
        ResultsPanel.SetActive(false);
    }
}
