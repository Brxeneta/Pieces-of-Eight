using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerChoiceHandlerAnimation, choiceAnimation;

    [SerializeField]
    private Animator Ship, enemyShip;

    [SerializeField]
    private Animator battleReview;

    public void ResetAnimations()
    {
        playerChoiceHandlerAnimation.Play("ShowHandler");
        choiceAnimation.Play("RemoveChoices");
    }

    public void PlayerMadeChoice()
    {
        playerChoiceHandlerAnimation.Play("RemoveHandler");
        choiceAnimation.Play("ShowChoices");
    }

    public void ResetShipAnimations()
    {
        Ship.Play("playerShipMotion");
        enemyShip.Play("enemyShipMotion");
    }

    public void playerShipFireAnimation()
    {
        Ship.Play("playerShipFire");
    }

    public void enemyShipFireAnimation()
    {
        enemyShip.Play("enemyShipFire");
    }

    public void battleReviewAnimation()
    {
        battleReview.Play("showBattleReview");
    }
}
