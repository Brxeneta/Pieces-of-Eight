using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private AnimationController animationController;
    private GameplayController gameplayController;

    private string playersChoice;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        gameplayController = GetComponent<GameplayController>();
    }

    public void GetChoice()
    {
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        print("Player Selected: " + choiceName);

        GameChoices selectedChoice = GameChoices.NONE;

        switch(choiceName)
        {
            case "Brace":
                selectedChoice = GameChoices.BRACE;
                break;
            case "Reload":
                selectedChoice = GameChoices.RELOAD;
                break;
            case "Fire":
                selectedChoice = GameChoices.FIRE;
                animationController.playerShipFireAnimation();
                break;
        }

        gameplayController.SetChoices(selectedChoice);
        animationController.PlayerMadeChoice();
        StartCoroutine(RestartAnimation());
    }

    IEnumerator RestartAnimation()
    {
        yield return new WaitForSeconds(1f);
        animationController.ResetShipAnimations();
    }
}
