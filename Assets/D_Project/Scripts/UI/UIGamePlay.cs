using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    public void OpenSetting()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
    }
}
