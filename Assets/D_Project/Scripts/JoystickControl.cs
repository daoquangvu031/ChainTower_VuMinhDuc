using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public static Vector3 direct;

    private Vector3 screen;

    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private Vector3 startPoint;
    private Vector3 updatePoint;

    public RectTransform joystickControl;
    public RectTransform joystickBG;
    public float magnitude;

    public GameObject joystickPanel;

    public static bool isGameplayCanvas = false;

    private bool active;


    // Start is called before the first frame update
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;

        joystickPanel.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (active && isGameplayCanvas)
        {
            updatePoint = MousePosition;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude((updatePoint - startPoint), magnitude) + startPoint;

            direct = (updatePoint - startPoint).normalized;
            direct.z = direct.y;
            direct.y = 0;
        }
    }

    public void ButtonDown()
    {
        startPoint = MousePosition;
        joystickBG.anchoredPosition = startPoint;
        joystickPanel.SetActive(true);
        active = true;
    }

    public void ButtonUp()
    {
        joystickPanel.SetActive(false);
        direct = Vector3.zero;
        active = false;
    }


    public static void DisableJoystick()
    {
        isGameplayCanvas = false;
    }

    public static void EnableJoystick()
    {
        isGameplayCanvas = true;
    }
}

