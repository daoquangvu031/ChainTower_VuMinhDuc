using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private LayerMask turretLayerMask;
    [SerializeField] private float maxBuildDistance = 1f;
    [SerializeField] private List<TurretData> turretDatas;
    [SerializeField] private GameObject buildError;


    private int currentTurretCost;
    private bool isBuilding = false;
    private TurretData selectedTurretData;
    private GameObject currentBuildError;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found.");
            return;
        }
    }

    private void Update()
    {
        if (isBuilding && Input.GetMouseButtonUp(0))
        {
            BuildTurret();
            isBuilding = false; // Đặt isBuilding thành false sau khi xây dựng
        }
    }

    public void MachineGunTurret()
    {
        selectedTurretData = turretDatas.FirstOrDefault(data => data.TurretType == TurretType.MachineGun);
        currentTurretCost = selectedTurretData.Coin;
        isBuilding = true;

    }

    public void LaserTower()
    {
        selectedTurretData = turretDatas.FirstOrDefault(data => data.TurretType == TurretType.LaserTower);
        currentTurretCost = selectedTurretData.Coin;
        isBuilding = true;
    }

    public void RocketTurret()
    {
        selectedTurretData = turretDatas.FirstOrDefault(data => data.TurretType == TurretType.RocketTower);
        currentTurretCost = selectedTurretData.Coin;
        isBuilding = true;
    }

    private IEnumerator HideBuildError()
    {
        yield return new WaitForSeconds(1.3f);

        if (currentBuildError != null)
        {
            Destroy(currentBuildError);
            isBuilding = false;
        }
    }


    private void BuildTurret()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 playerPosition = player.transform.position;

            Vector3 playerForward = player.transform.forward;

            Vector3 buildPosition = playerPosition + playerForward * 12f;

            buildPosition.y = 2.5f;

            float buildDistanceToPlayer = Vector3.Distance(buildPosition, playerPosition);

            if (buildDistanceToPlayer > maxBuildDistance)
            {
                Debug.Log("Vị trí xây dựng quá xa!");
                isBuilding = false;
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(buildPosition, 2f); 
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(Constant.TAG_TURRET))
                {
                    Debug.Log("Vị trí xây dựng bị chồng lấn!");
                    if (buildError != null)
                    {
                        if (currentBuildError != null)
                        {
                            Destroy(currentBuildError);
                        }

                        currentBuildError = Instantiate(buildError, buildPosition, Quaternion.identity);
                        isBuilding = true;
                        StartCoroutine(HideBuildError());
                    }

                    isBuilding = false;
                    return;
                }
            }

            int turretCost = selectedTurretData.Coin;

            if (player.GetComponent<Player>().currentCoins >= turretCost)
            {
                GameObject currentTurretPrefab = selectedTurretData.Prefab.gameObject;
                Instantiate(currentTurretPrefab, buildPosition, Quaternion.identity);

                // Giảm số tiền của người chơi tương ứng
                player.GetComponent<Player>().currentCoins -= turretCost;
                player.GetComponent<Player>().UpdateCoinText();
            }
            else
            {
                Debug.Log("Không đủ tiền để xây trụ súng!");
                isBuilding = false;
                return;
            }

            GameObject turretPrefab = selectedTurretData.Prefab.gameObject;
            
            isBuilding = false; 
        }
    }
}
