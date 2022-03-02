using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompliteCanvas;
    [SerializeField] private GameObject finishUI;
    private bool _isActivated = false;


    public void Activate()
    {
        _isActivated = true;
        finishUI.SetActive(false);
    }

    public void FinishLevel()
    {
        if(_isActivated)
        {
            levelCompliteCanvas.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            finishUI.SetActive(true);
        }
    }
}
