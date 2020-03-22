using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public LayerMask itemLayerMask;
    public GameObject pickupDialog;
    public GameObject prankWood;

    private bool isPassedLevel;
    private int currentWoodCount = 0;

    private Item _currentItemSelection;

    private void GetItem(Item item)
    {
        if (item.itemName == "wood")
        {
            currentWoodCount++;
        }
        Destroy(item.gameObject);
    }

    private void Update()
    {

        CheckCollectionPassedLevel();

        CheckItemInteract();

        pickupDialog.SetActive(_currentItemSelection != null);

        if (Input.GetKeyDown(KeyCode.E) && _currentItemSelection != null)
        {
            GetItem(_currentItemSelection);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPrankWood();
        }
    }

    private void CheckCollectionPassedLevel()
    {
        if (isPassedLevel) return;
        if (currentWoodCount >= 4)
        {
            isPassedLevel = true;
            GameLevelSystem.Instance.PassLevel(4);
        }
    }

    private void CheckItemInteract()
    {

        if (_currentItemSelection != null) _currentItemSelection = null;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int distance = 2;
        if (Physics.Raycast(ray, out hit, distance, itemLayerMask))
        {
            _currentItemSelection = hit.transform.GetComponent<Item>();
        }
    }

    public void SpawnPrankWood()
    {
        if (currentWoodCount < 4) return;
        currentWoodCount = 0;
        Vector3 placeTarget = transform.position + transform.forward * 1.5f;
        placeTarget.y += 0.2f;
        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, rotation.eulerAngles.z);
        Instantiate(prankWood, placeTarget, rotation);
    }

    public int GetCurrentWood()
    {
        return currentWoodCount;
    }

}
