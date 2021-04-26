using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Game;
    public GameObject TutorialControl;

    public void HiddenTutorial()
    {
        Game.SetActive(true);
        TutorialControl.SetActive(false);
    }
}
