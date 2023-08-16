using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public List<Bot> bots = new List<Bot>();

    public void AddBot(Bot bot)
    {
        bots.Add(bot);
    }

    public void RemoveBot(Bot bot)
    {
        bots.Remove(bot);
        Destroy(bot.gameObject);
    }

    public void RemoveAllBots()
    {   
        foreach (Bot bot in bots)
        {
            bot.Die(); // Gọi hàm xóa bot trong script của bot
        }
        bots.Clear();
    }
}