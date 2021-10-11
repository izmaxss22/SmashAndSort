using System;
using UnityEngine;

public class AppLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        InitAds();
    }

    private void Start()
    {
        //todo
        // DataManager.Instance.Set_EnergyCount(300);
        InitPlane();
        InitRateScreen();
    }

    // Метод принимающий решение о вызове окна оценки
    private void InitRateScreen()
    {
        int appLaunchCounts = DataManager.Instance.GetGameLaunchCounts();
        //  Если это не первый запуск
        if (appLaunchCounts >= 1)
        {
            int callsRateScreenCount = DataManager.Instance.GetRateScreenCallsCount();
            // Если вызовов окна оценки было меньше 4
            if (callsRateScreenCount < 4)
            {
                callsRateScreenCount++;
                DataManager.Instance.SetRateScreenCallsCount(callsRateScreenCount);
                //todo
                //StoreReview.RequestRating();
            }
        }
        appLaunchCounts++;
        DataManager.Instance.SetGameLaunchCounts(appLaunchCounts);
    }

    // Установка данных для игрового плэйна
    private void InitPlane()
    {
        // Создание, установка маштаба (исходя из размера экрана), установка положения
        var camera = Camera.main;
        float height = camera.pixelHeight;
        float width = camera.pixelWidth + 100;
        Vector3 leftSidePoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 rightSidePoint = Camera.main.ScreenToWorldPoint(new Vector3(width, 0));
        Vector3 topSidePoint = Camera.main.ScreenToWorldPoint(new Vector3(0, height));
        Vector3 downSidePoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        float xScale = Math.Abs(rightSidePoint.x - leftSidePoint.x);
        float zScale = topSidePoint.z - downSidePoint.z;
        var planePos = Camera.main.transform.position;
        planePos.y = 0;
        var plane = GameObject.Find("itemsPlane");
        plane.transform.position = planePos;
        plane.transform.localScale = new Vector3(xScale, 1, zScale) / 9.7f;
    }

    // Инициализация рекламы
    private void InitAds()
    {
        //bool testMode = false;
        //TODO перед релизом уберать тестовый мод и этот код
        //Advertisement.Initialize(DataManager.Instance.gameId_appStore.ToString(), testMode);
        // Если реклама не готова
        //if (!Advertisement.IsReady())
        //{
        //    if (Application.platform == RuntimePlatform.Android)
        //        Advertisement.Initialize(DataManager.Instance.gameId_googlePLay.ToString(), testMode);
        //    // todo Просходит ли проверка дял ipad (потомучто айфон плеер это не ios а чисто айфон???)
        //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //    {
        //        Advertisement.Initialize(DataManager.Instance.gameId_appStore.ToString(), testMode);
        //    }
        //}
    }
}
