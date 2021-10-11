using System;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class MapItemsManager : MonoBehaviour
{
    private Camera mainCamera;
    public Transform collidersCont;
    public GameObject prefabFor_borderColliders;

    public Transform contForCreatedItems;
    private List<GameObject> createdColliders = new List<GameObject>();

    private List<GameObject> createdItems = new List<GameObject>();

    public Material planeMat;
    public Texture[] texturesForPlane;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void CreateMapItems(DataManager.LevelData levelData)
    {
        CameraShaker cameraShaker = Camera.main.GetComponent<CameraShaker>();
        cameraShaker.enabled = false;

        mainCamera.transform.position = levelData.GetCameraPos();
        mainCamera.orthographicSize = levelData.cameraScale;

        cameraShaker.RestPositionOffset = levelData.GetCameraPos();
        cameraShaker.enabled = true;

        CreatePlane();
        CreateBorderColiders();
        CreateItems(levelData);
        SetPlaneSkin();
    }

    // Создание плэйна
    private void CreatePlane()
    {
        var planePos = Camera.main.transform.position;
        planePos.y = 0;
        var plane = GameObject.Find("itemsPlane");
        plane.transform.position = planePos;
    }

    // Удаление всех созданных элементов уровня с карты
    public void DeletaMapItems()
    {
        foreach (var item in createdItems)
        {
            Destroy(item);
        }
        createdItems.Clear();

        foreach (var item in createdColliders)
        {
            Destroy(item);
        }
        createdColliders.Clear();
    }

    // Создание элементов уровня
    private void CreateItems(DataManager.LevelData levelData)
    {
        var prefabs = DataManager.Instance.prefabsForGameItems;

        foreach (var item in levelData.items)
        {
            var createdItem = Instantiate(
            prefabs[item.itemId],
            item.GetPositon(),
            item.GetRotationInEuler(),
            contForCreatedItems
            );
            createdItem.name = item.itemId.ToString();
            // todo добавление спец данных
            createdItems.Add(createdItem);
        }
    }

    // Создание колайдеров по краям экрана (чтобы кубики не вылетали за грань)
    private void CreateBorderColiders()
    {
        Vector3 leftSideCollider = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f));
        leftSideCollider.x -= 2f;
        leftSideCollider.y = 20;
        GameObject leftCollider = Instantiate(prefabFor_borderColliders, leftSideCollider, Quaternion.Euler(0, 0, 0), collidersCont);
        createdColliders.Add(leftCollider);

        Vector3 rightSideCollider = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f));
        rightSideCollider.x += 2f;
        rightSideCollider.y = 20;
        GameObject rightCollider = Instantiate(prefabFor_borderColliders, rightSideCollider, Quaternion.Euler(0, 0, 0), collidersCont);
        createdColliders.Add(rightCollider);

        Vector3 topSideCollider = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1));
        topSideCollider.z += 2f;
        topSideCollider.y = 20;
        GameObject topCollider = Instantiate(prefabFor_borderColliders, topSideCollider, Quaternion.Euler(0, 90, 0), collidersCont);
        createdColliders.Add(topCollider);

        Vector3 downSideCollider = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0));
        downSideCollider.z -= 2f;
        downSideCollider.y = 20;
        GameObject downCollider = Instantiate(prefabFor_borderColliders, downSideCollider, Quaternion.Euler(0, 90, 0), collidersCont);
        createdColliders.Add(downCollider);
    }

    // Установка матерьяла для плэйна бэкграунда
    private void SetPlaneSkin()
    {
        int number = DataManager.Instance.GetPickedPlaneSkinNumber();
        planeMat.mainTexture = texturesForPlane[number];

        if (number == 12)
        {
            number = 0;
        }
        else
        {
            number++;
        }

        DataManager.Instance.SetPickedPlaneSkinNumber(number);
    }
}
