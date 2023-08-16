using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    public UnityEvent onPlayGame = new UnityEvent();

    public void PlayGame()
    {
        JoystickControl.EnableJoystick();
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIGamePlay>();
        onPlayGame.Invoke(); 
    }
}
