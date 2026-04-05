using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject rapidTowerPrefab;
    [SerializeField] private GameObject heavyTowerPrefab;
    [SerializeField] private GameObject slowTowerPrefab;

    private GameObject selectedTowerPrefab;
    private Camera mainCamera;

    // Store occupied spots
    private System.Collections.Generic.List<Vector3>
        occupiedSpots = new System.Collections.Generic
        .List<Vector3>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        if (Input.GetMouseButtonDown(0))
            TryPlaceTower();
    }

    public void SelectRapidTower() =>
        selectedTowerPrefab = rapidTowerPrefab;
    public void SelectHeavyTower() =>
        selectedTowerPrefab = heavyTowerPrefab;
    public void SelectSlowTower() =>
        selectedTowerPrefab = slowTowerPrefab;

    private void TryPlaceTower()
    {
        Ray ray = mainCamera.ScreenPointToRay(
            Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit)) return;

        // Click on existing tower - show info
        if (hit.collider.CompareTag("Tower"))
        {
            Tower existingTower =
                hit.collider.GetComponent<Tower>();
            if (existingTower == null)
                existingTower = hit.collider
                    .GetComponentInParent<Tower>();
            if (existingTower != null)
                UIManager.Instance.ShowTowerInfo(existingTower);
            return;
        }

        // Click on tower spot - place tower
        if (hit.collider.CompareTag("TowerSpot"))
        {
            if (selectedTowerPrefab == null) return;

            Vector3 spotPos =
                hit.collider.transform.position;

            // Check if spot is already occupied
            foreach (Vector3 occupied in occupiedSpots)
            {
                if (Vector3.Distance(spotPos, occupied) < 1f)
                    return;
            }

            Tower tower = selectedTowerPrefab
                .GetComponent<Tower>();
            if (tower == null) return;

            if (!GameManager.Instance.SpendGold(
                tower.GetCost())) return;

            Vector3 pos = spotPos;
            pos.y += 0.5f;
            Instantiate(selectedTowerPrefab, pos,
                Quaternion.identity);
            AudioManager.Instance?.PlayPlaceTower();

            // Mark spot as occupied
            occupiedSpots.Add(spotPos);
        }
    }

    public void DeselectTower() =>
        selectedTowerPrefab = null;
}