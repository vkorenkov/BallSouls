using System.Collections;
using System.Collections.Generic;
using Homewor11.InputControl;
using UnityEngine;
using UnityEngine.UI;

namespace Homewor11.InputControl
{
    [RequireComponent(typeof(PlayerControl))]
    public class PlayerInputs : MonoBehaviour
    {
        public Rigidbody rb;
        public bool isCamera = true;

        [SerializeField] bool animationOnObjects = true;

        public Vector3 startPosition;
        /// <summary>
        /// Экземпляр класса перемещения игрока
        /// </summary>
        PlayerControl moveCharacter;
        /// <summary>
        /// Экземпляр класса перемещения камеры
        /// </summary>
        ThirdPersonCamera thirdPersonCamera;
        /// <summary>
        /// Вектор движения игрока
        /// </summary>
        Vector3 move;
        /// <summary>
        /// Угол поворота камеры
        /// </summary>
        float rotate;
        /// <summary>
        /// Вектор прыжка игрока
        /// </summary>
        Vector3 jump;
        /// <summary>
        /// Перемещение мыши по оси X
        /// </summary>
        float moveMouseX;
        /// <summary>
        /// Перемещение мыши по оси Y
        /// </summary>
        float MoveMouseY;
        /// <summary>
        /// Камера 
        /// </summary>
        Camera mainCamera;
        /// <summary>
        /// Сглаживание(плавность) движения камерыs
        /// </summary>
        [SerializeField] float rotationSmoothTime = 0.12f;
        /// <summary>
        /// Временной интервал сглаживания движения камеры
        /// </summary>
        public float turnSmoothTime = 0.0f;

        public bool isControlActive = true;

        float tempCameraYPosition;
        float tempDistanceFromTarget;

        [SerializeField] bool isSphere;

        RespawnScript room;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Инициализация объекта класса перемещения игрока
            moveCharacter = GetComponent<PlayerControl>();
            // Инициализация объекта класса перемещения камеры
            thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            // Получение главной камеры
            mainCamera = Camera.main;
            // Получение высоты положения камеры для "приседания"
            tempCameraYPosition = moveCharacter.cameraYPosition;
            // Получене дистации от камеры до объекта слежения для камеры "от первого лица" в "присяде".
            tempDistanceFromTarget = thirdPersonCamera.distanceFromTarget;

            rb = GetComponent<Rigidbody>();

            startPosition = rb.transform.position;

            room = GetComponent<RespawnScript>();
        }

        void Update()
        {
            ChangeRoomInput();

            // Получение вектора перемещения игрока
            move = new Vector3(Input.GetAxis(GlobalInput.Horizontal_Axis), 0, Input.GetAxis(GlobalInput.Vertical_Axis));

            // Получение Перемещения мыши по оси X
            moveMouseX += Input.GetAxis(GlobalInput.Mouse_X);
            // Получение Перемещения мыши по оси X
            MoveMouseY -= Input.GetAxis(GlobalInput.Mouse_Y);

            // получение вектора прыжка персонажа
            if (Input.GetButtonDown(GlobalInput.Jump_Button))
                jump = Vector3.up;

            // Приседание
            //if (Input.GetButton(GlobalInput.Sitdown))
            if (Input.GetKey(KeyCode.LeftControl))
            {
                moveCharacter.cameraYPosition = 0;
                thirdPersonCamera.distanceFromTarget = 0;
            }
            else
            {
                moveCharacter.cameraYPosition = tempCameraYPosition;
                thirdPersonCamera.distanceFromTarget = tempDistanceFromTarget;
            }

            // Получение пересечения луча с объектом
            thirdPersonCamera.GetRayHit();
        }

        private void FixedUpdate()
        {
            if (isControlActive)
            {
                // Вызов метода перемещения игрока
                moveCharacter.MoveCharacter(move, mainCamera);

                // Если вектор перемещения не равен стандртному значения вектора
                if (!move.normalized.Equals(Vector3.zero))
                {
                    // Получение угла вращения камеры
                    rotate = mainCamera.transform.eulerAngles.y;

                    if (!isSphere)
                        // Вызов перегрузки метода с параметрами угла вращения камеры и времени сглаживания поворота камеры (Используется для всех объектов кроме сферы)
                        moveCharacter.RotateCharacter(rotate, turnSmoothTime);
                }

                // Вызов метода прыжка игрока
                moveCharacter.JumpCharacter(jump);

                // Сброс вектора прыжка
                jump = new Vector3();
            }

            if (!isCamera) thirdPersonCamera.RotateObject(moveMouseX, MoveMouseY, 0.2f);
        }

        private void LateUpdate()
        {
            // Вызов метода поворота камеры
            if(isCamera) thirdPersonCamera.RotateCamera(moveMouseX, MoveMouseY, rotationSmoothTime);

            // Вызов метода реакции камеры на препятствия
            thirdPersonCamera.ObstaclesReact();

            // Вызов метода реакции камеры на модель игрока
            thirdPersonCamera.PlayerReact();
        }

        void ChangeRoomInput()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
                room.ChangeRoom(0, animationOnObjects);
            if (Input.GetKeyUp(KeyCode.Alpha2))
                room.ChangeRoom(1, animationOnObjects);
            if (Input.GetKeyUp(KeyCode.Alpha3))
                room.ChangeRoom(2, animationOnObjects);
            if (Input.GetKeyUp(KeyCode.Alpha4))
                room.ChangeRoom(3, animationOnObjects);
        }        
    }
}
