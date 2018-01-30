using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VirtualJoyStick : MonoBehaviour, IDragHandler,IPointerUpHandler,IPointerDownHandler {

    private Image bgImag;
    private Image joystickImg;
    private Vector3 inputVector;
    public bool isTouched=false;

    void Start()
    {
        bgImag = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImag.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
           // Debug.Log(pos);
            pos.x = (pos.x / bgImag.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImag.rectTransform.sizeDelta.y);
           // Debug.Log("a:"+pos);
            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImag.rectTransform.sizeDelta.x/2.5f), inputVector.z * (bgImag.rectTransform.sizeDelta.y/2.5f));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        isTouched = true;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        isTouched = false;
       // Debug.Log("InPointerUp");
        //inputVector = Vector3.zero;
        //joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }



}
