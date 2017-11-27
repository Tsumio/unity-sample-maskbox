using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanManager : MonoBehaviour {

    ////=============================================================================
    //// Local Field
    ////  
    ////=============================================================================
    private Animator  humanAnimator;
    private Scrollbar scrollBar;
    private float     CALL_TIME;

    public Animator HumanAnimator {
        get {
            return humanAnimator;
        }

        set {
            humanAnimator = value;
        }
    }

    public Scrollbar ScrollBar {
        get {
            return scrollBar;
        }

        set {
            scrollBar = value;
        }
    }

    ////=============================================================================
    //// Animation Hash
    ////  These are for animation.Static and Read only.
    ////=============================================================================
    readonly static int ANI_HASH_ANGRY = Animator.StringToHash("Base Layer.Angry");
    readonly static int ANI_HASH_SMILE = Animator.StringToHash("Base Layer.Smile");


    ////=============================================================================
    //// MonoBehaviour
    ////  Basic function.
    ////=============================================================================

    private void Start() {
        this.InitializeComponents();
        this.InitializeLocalField();
    }

    private void InitializeComponents() {
        HumanAnimator = GetComponent<Animator>();
        ScrollBar     = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
    }

    private void InitializeLocalField() {
        CALL_TIME = 1.0f;//For coroutine.
    }

    ////=============================================================================
    //// Animation
    ////  Operate human by animation.
    ////=============================================================================

    public void StartSmileAimation() {
        if(this.IsAnimationSmile()) {
            return;
        }
        HumanAnimator.SetTrigger("SmileTrigger");
        StartCoroutine(CallStartAngryAnimation(CALL_TIME, ScrollBar.value));
    }

    public void StartAngryAnimation() {
        if(this.IsAnimationAngry()) {
            return;
        }
        
        HumanAnimator.SetTrigger("AngryTrigger");
    }

    private IEnumerator CallStartAngryAnimation(float waitTime, float prevValue) {
        Debug.Log("Start: CallStartAngryAnimation");
        yield return new WaitForSeconds(waitTime);

        //Get currentValue
        float currentValue = ScrollBar.value;
        //Compare the current value with the previous value
        if(currentValue == prevValue) {
            StartAngryAnimation();
        } else {
            StartCoroutine(CallStartAngryAnimation(CALL_TIME, ScrollBar.value));
        }
    }

    private bool IsAnimationSmile() {
        AnimatorStateInfo stateInfo = HumanAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.fullPathHash == ANI_HASH_SMILE) {
            return true;
        }

        return false;
    }

    private bool IsAnimationAngry() {
        AnimatorStateInfo stateInfo = HumanAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.fullPathHash == ANI_HASH_ANGRY) {
            return true;
        }

        return false;
    }
}