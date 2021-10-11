using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startMousePosition;
    private float clickDeltaY;
    private float clickDeltaX;
    private Vector3 playerVelocity;
    private bool swipeIsMaded;

    private float playerSpeed;
    private int idForPlayerMovementMode;
    public Animator animator;

    public bool isCanMove = true;

    private VibrationManager VibrationManager;
    public void Init(GameManager.GameData gameData)
    {
        VibrationManager = VibrationManager.Instance;

        rb = GetComponent<Rigidbody>();
        playerSpeed = gameData.levelDatas[gameData.subLevelInProgressNumber].playerSpeed;
        idForPlayerMovementMode = gameData.levelDatas[gameData.subLevelInProgressNumber]
            .idForPlayerMovementMode;
    }

    public void SetMovementState(bool state)
    {
        if (state == true)
        {
            isCanMove = true;
        }
        else
        {
            isCanMove = false;
            playerVelocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (isCanMove)
        {
            if (!Check_CursorIsUnderUI())
            {
                MovingWithDrag();
                GameManager.Instance.OnPlayerMove();
            }
            // Чобы остановился когд поднял палец находсь поверх ui
            if (Input.GetMouseButtonUp(0) && idForPlayerMovementMode == 0)
            {
                playerVelocity = new Vector3(0, 0, 0);
            }
        }
    }

    private bool Check_CursorIsUnderUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void FixedUpdate()
    {
        // Перемещение персонажа исходя из заданного направления
        rb.velocity = playerVelocity * 1.65f;
    }



    private void MovingWithDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Animator>().SetTrigger("onMove");
            startMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            clickDeltaY = startMousePosition.y - Input.mousePosition.y;
            clickDeltaY = -clickDeltaY;

            clickDeltaX = startMousePosition.x - Input.mousePosition.x;
            clickDeltaX = -clickDeltaX;

            if (Mathf.Abs(clickDeltaX) > 1 || Mathf.Abs(clickDeltaY) > 1)
            {
                playerVelocity = new Vector3(clickDeltaX, 0, clickDeltaY);
                startMousePosition = Input.mousePosition;
            }
            else
            {
                playerVelocity = Vector3.zero;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Animator>().SetTrigger("onStay");
            playerVelocity = new Vector3(0, 0, 0);
        }
    }

    //private void MovingWithDrag()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        gameObject.GetComponent<Animator>().SetTrigger("onMove");
    //        startMousePosition = Input.mousePosition;
    //        Vector3 circlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        circlePos.y = 1;
    //        joystickCircle.transform.position = circlePos;
    //        joystickCircle.SetActive(true);
    //    }
    //    else if (Input.GetMouseButton(0))
    //    {
    //        //clickDeltaY = startMousePosition.y - Input.mousePosition.y;
    //        //clickDeltaY = -clickDeltaY;

    //        //clickDeltaX = startMousePosition.x - Input.mousePosition.x;
    //        //clickDeltaX = -clickDeltaX;
    //        ////Debug.Log((startMousePosition - Input.mousePosition).magnitude);
    //        ////Debug.Log((startMousePosition - Input.mousePosition).normalized);
    //        ////if ((startMousePosition - Input.mousePosition).magnitude > 10)
    //        ////{
    //        //Vector3 pos = -(startMousePosition - Input.mousePosition).normalized;
    //        ////startMousePosition = Input.mousePosition;
    //        //if (pos.x != 0 && pos.y != 0)
    //        //{
    //        //    playerVelocity = new Vector3(pos.x, 0, pos.y) * 0.5f;
    //        //}
    //        ////}

    //        //var vectorNorm = (Input.mousePosition - startMousePosition).normalized;
    //        Vector3 controlVector = Input.mousePosition - startMousePosition;
    //        Vector3 normControlVector = controlVector.normalized;
    //        //if (controlVector.magnitude > 0.01f)
    //        //{
    //        //    float relativeLenght = controlVector.magnitude;
    //        //    //float relativeLenght = controlVector.magnitude / (Screen.width * 0.5f);
    //        //    //float speed = 2 * relativeLenght;
    //        //    float speed = controlVector.magnitude / 300;
    //        //    Debug.Log(speed);
    //        //    speed = Mathf.Clamp(speed, 0, 3.5f);
    //        //    Debug.Log(speed);
    //        //    playerVelocity = new Vector3(normControlVector.x, 0, normControlVector.y) * speed;
    //        //}

    //        //ПОСТОЯННАЯ СКОРОСТЬ
    //        if (controlVector.magnitude > 0.01f)
    //        {
    //            float relativeLenght = controlVector.magnitude;
    //            float speed = controlVector.magnitude / 100;
    //            //speed = Mathf.Clamp(0, 1, speed);
    //            if (controlVector.magnitude > 350)
    //            {
    //                if (vibrate != 1)
    //                {
    //                    VibrationManager.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Warning);
    //                    vibrate = 1;
    //                }
    //                speed = 1.7f;
    //            }
    //            else
    //            {
    //                if (vibrate != 0)
    //                {
    //                    vibrate = 0;
    //                }
    //                speed = 1.1f;
    //            }
    //            playerVelocity = new Vector3(normControlVector.x, 0, normControlVector.y) * speed;
    //        }
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        joystickCircle.SetActive(false);

    //        gameObject.GetComponent<Animator>().SetTrigger("onStay");
    //        playerVelocity = new Vector3(0, 0, 0);
    //    }
    //}
    // Передвижение с помощью двиения пальца дял задачи направления и остановки по поднятию пальца
    //private void MovingWithDrag()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        gameObject.GetComponent<Animator>().SetTrigger("onMove");
    //        startMousePosition = Input.mousePosition;
    //    }
    //    else if (Input.GetMouseButton(0))
    //    {
    //        clickDeltaY = startMousePosition.y - Input.mousePosition.y;
    //        clickDeltaY = -clickDeltaY;

    //        clickDeltaX = startMousePosition.x - Input.mousePosition.x;
    //        clickDeltaX = -clickDeltaX;

    //        Debug.Log(Input.mousePosition);

    //        if (clickDeltaY >= 100)
    //        {
    //            startMousePosition = Input.mousePosition;
    //            playerVelocity = new Vector3(0, 0, 1);
    //        }
    //        // Вниз
    //        if (clickDeltaY <= -100)
    //        {
    //            startMousePosition = Input.mousePosition;
    //            playerVelocity = new Vector3(0, 0, -1);
    //        }
    //        // Вправо
    //        if (clickDeltaX >= 100)
    //        {
    //            startMousePosition = Input.mousePosition;
    //            playerVelocity = new Vector3(1, 0, 0);
    //        }
    //        // Влево
    //        if (clickDeltaX <= -100)
    //        {
    //            startMousePosition = Input.mousePosition;
    //            playerVelocity = new Vector3(-1, 0, 0);
    //        }
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        gameObject.GetComponent<Animator>().SetTrigger("onStay");
    //        playerVelocity = new Vector3(0, 0, 0);
    //    }
    //}
    //private void TestMoveForTestingParticles()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        playerVelocity = new Vector3(1, 0, 0);
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //        playerVelocity = new Vector3(-1, 0, 0);
    //    if (Input.GetKeyDown(KeyCode.W))

    //        playerVelocity = new Vector3(0, 0, 1);
    //    if (Input.GetKeyDown(KeyCode.S))

    //        playerVelocity = new Vector3(0, 0, -1);
    //}
}
