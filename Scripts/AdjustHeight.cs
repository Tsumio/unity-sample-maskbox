using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustHeight : MonoBehaviour {

    ////=============================================================================
    //// Local Field
    ////  
    ////=============================================================================
    public int minHeight = 180;
    private RectTransform rectTrans;
    private Text mainText;
    

    public int MinHeight {
        get {
            return minHeight;
        }
    }

    public RectTransform RectTrans {
        get {
            return rectTrans;
        }

        set {
            rectTrans = value;
        }
    }

    public Text MainText {
        get {
            return mainText;
        }

        set {
            mainText = value;
        }
    }


    ////=============================================================================
    //// MonoBehaviour
    ////  Basic function.
    ////=============================================================================
    private void Start() {
        this.InitializeComponents();
        this.SetAppropriateHeight();
    }

    private void InitializeComponents() {
        RectTrans = GetComponent<RectTransform>();
        MainText  = GetComponent<Text>();
    }

    private void Update() {
        
    }

    ////=============================================================================
    //// Main Text Function.
    ////  Get text breaks and set appropriate height.
    ////=============================================================================
    private void SetAppropriateHeight() {
        RectTrans.sizeDelta = new Vector2(RectTrans.rect.width, MainText.preferredHeight);
        this.SetMinTextHeight();
        this.FitScrollBarValue();
    }

    private void FitScrollBarValue() {
        Scrollbar bar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        bar.value = 1.0f;
    }


    ////=============================================================================
    //// Rect Function.
    ////  Get and Set the height of the rect.
    ////=============================================================================
    private bool IsHeightSmallerThanMinHeight() {
        return RectTrans.rect.height < MinHeight;
    }

    private void SetMinTextHeight() {
        if(this.IsHeightSmallerThanMinHeight()) {
            RectTrans.sizeDelta = new Vector2(RectTrans.rect.width, MinHeight);
        }
    }
}
