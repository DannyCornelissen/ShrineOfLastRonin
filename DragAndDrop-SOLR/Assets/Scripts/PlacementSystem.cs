using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private MousePositionRetriever mousePositionRetriever;
    [SerializeField] private Grid grid;
    [SerializeField] private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;
    private GridData towerData = new GridData();

    private Renderer previewRenderer = new Renderer();

    private List<GameObject> placedGameObjects = new();

    private void start()
    {
        StopPlacement();
        //towerData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int id)
    {
        StopPlacement();
        selectedObjectIndex = database.ObjectsData.FindIndex(data => data.ID == id);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found like: {id}");
        }
        cellIndicator.SetActive(true);
        mousePositionRetriever.OnClicked += PlaceTower;
    }

    private void PlaceTower()
    {
        if (mousePositionRetriever.IsMouseOverUI())
        {
            return;
        }
        Vector3 mousePosition = mousePositionRetriever.GetMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
        {
            return;
        }
        GameObject newObject = Instantiate(database.ObjectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        newObject.transform.position += new Vector3(0.5f, 0, 0.5f);
        placedGameObjects.Add(newObject);
        GridData selectedData = database.ObjectsData[selectedObjectIndex].ID >= 0 ? towerData : null;
        selectedData.AddObjectAt(gridPosition, database.ObjectsData[selectedObjectIndex].Size, database.ObjectsData[selectedObjectIndex].ID, placedGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int i)
    {
        GridData selectedData = towerData;
        return selectedData.CanPlaceObjectAt(gridPosition, database.ObjectsData[i].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;

        cellIndicator.SetActive(false);
        mousePositionRetriever.OnClicked -= PlaceTower;
    }

    private void Update()
    {

        Vector3 mousePosition = mousePositionRetriever.GetMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        cellIndicator.transform.position += new Vector3(0.5f, 0, 0.5f);

        if (selectedObjectIndex < 0)
            return;

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        Debug.Log("this is 1:"+ placementValidity);
        Debug.Log("this is 2:"+ CheckPlacementValidity(gridPosition, selectedObjectIndex));
        //previewRenderer.material.color = placementValidity ? Color.white : Color.red;

    }
}
