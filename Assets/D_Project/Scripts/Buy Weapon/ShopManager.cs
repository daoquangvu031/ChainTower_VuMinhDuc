using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask turretLayerMask;
    [SerializeField] private float maxBuildDistance = 1f;
    [SerializeField] private List<TurretData> turretDatas;

    public TurretData selectedTurretData;

    private bool isBuilding = false;

    private void Update()
    {
        if (isBuilding && Input.GetMouseButtonDown(0))
        {
            BuildTurret();
        }
    }

    public void MachineGunTurret()
    {
        selectedTurretData = turretDatas.FirstOrDefault(data => data.TurrretType == TurrretType.MachineGun);
        isBuilding = true;

    }

    public void LaserTower()
    {
        selectedTurretData = turretDatas.FirstOrDefault(data => data.TurrretType == TurrretType.LaserTower);
        isBuilding = true;
    }

    private void BuildTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 buildPosition = ray.GetPoint(distance);

            Vector3 playerPosition = player.transform.position;

            float buildDistanceToPlayer = Vector3.Distance(buildPosition, playerPosition);

            if (buildDistanceToPlayer > maxBuildDistance)
            {
                Debug.Log("Vị trí xây dựng quá xa!");
                isBuilding = false;
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(buildPosition, 2f); // Đặt bán kính kiểm tra
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Turret"))
                {
                    Debug.Log("Vị trí xây dựng bị chồng lấn!");
                    isBuilding = false;
                    return;
                }
            }

            GameObject turretPrefab = selectedTurretData.Prefab.gameObject;
            
            Instantiate(turretPrefab, buildPosition, Quaternion.identity);
            
            isBuilding = false; 
        }
    }
}
