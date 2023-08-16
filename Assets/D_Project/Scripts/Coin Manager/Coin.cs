using System.Collections;
using System.Collections.Generic;
using TowerDefense.Game;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    public static List<Coin> coinsInScene = new List<Coin>();


    public void Start()
    {
        coinsInScene.Add(this); // Thêm đồng xu vào danh sách khi nó được tạo ra
    }

    public void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public void RemoveFromList()
    {
        coinsInScene.Remove(this);
    }

    public void EatCoin()
    {
        RemoveFromList();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (UIDefeat.defeatCanvasDisplayed)
        {
            RemoveFromList();
        }
    }
}
