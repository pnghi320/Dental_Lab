using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public Button firstButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = firstButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
  
    void TaskOnClick()
    {
        TutorialManager.instance.Step = TutorialManager.instance.Step+1;
        Debug.Log("You have clicked the button!" + TutorialManager.instance.Step.ToString());
    }
}
