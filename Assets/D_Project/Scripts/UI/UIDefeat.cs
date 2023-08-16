using System.Collections;
using System.Collections.Generic;
using TowerDefense.Game;
using UnityEngine;

public class UIDefeat : UICanvas
{
    public static bool defeatCanvasDisplayed = false;

    public void Home()
    {

        UIManager.Ins.OpenUI<UIMainMenu>();
        UIManager.Ins.CloseUI<UIDefeat>();

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = player.initialPosition; 
            player.Reset();
        }
        TurretBase[] turrets = FindObjectsOfType<TurretBase>();
        foreach (var turret in turrets)
        {
            Destroy(turret.gameObject);
        }
        foreach (var coin in Coin.coinsInScene)
        {
            Destroy(coin.gameObject);
        }
        Coin.coinsInScene.Clear();
    }

}
