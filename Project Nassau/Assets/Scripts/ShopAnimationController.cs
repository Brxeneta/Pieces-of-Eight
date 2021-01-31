using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator panelAnimation;

    [SerializeField]
    private Animator iscrollsPanelAnimation;

    [SerializeField]
    private Animator scrollsPanelAnimation;
    public void HidePanel()
    {
        panelAnimation.Play("hideRiddlePanel");
    }

    public void ShowPanel()
    {
        panelAnimation.Play("showRiddlePanel");
    }

    public void showInsufficentScrolls()
    {
        iscrollsPanelAnimation.Play("showPanel");
    }

    public void hideInsufficentScrolls()
    {
        iscrollsPanelAnimation.Play("hidePanel");
    }

    public void showEndGameMessage()
    {
        scrollsPanelAnimation.Play("showEnd");
    }

    public void hideEndGameMessage() {
        scrollsPanelAnimation.Play("hideEnd");
    }
}
