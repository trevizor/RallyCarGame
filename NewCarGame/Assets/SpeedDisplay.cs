using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{
    public Rigidbody target;
    public Car targetCar;
    // Start is called before the first frame update
    void Start()
    {
        targetCar = target.gameObject.GetComponent<Car>();
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 6 / 100);
        style.alignment = TextAnchor.LowerRight;
        style.fontSize = h * 6 / 100;
        style.normal.textColor = new Color(1.0f, 0.0f, 0.3f, 1.0f);
        string text = (targetCar.CurrentGear+1) + " / " + Mathf.Round(target.velocity.magnitude).ToString();
        GUI.Label(rect, text, style);
    }
}
