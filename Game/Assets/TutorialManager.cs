using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator tutorialAnimator;
    public TextMeshProUGUI prefaceText;
    public TextMeshProUGUI contentText;
    public Tutorial[] tutorials;


    public void ShowTutorial(int level) {
        foreach (Tutorial t in tutorials) {
            if (t.level == level) {
                prefaceText.text = t.preface;
                contentText.text = t.content;
                tutorialAnimator.Play("FadeInOut");
            }
        }
    }
}
