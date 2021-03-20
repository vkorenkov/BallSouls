using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Homewor11.InputControl
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        /// <summary>
        /// Полк главной камеры
        /// </summary>
        Camera cam;

        [SerializeField] Text textDescription;
        /// <summary>
        /// Чувстывительность мыши
        /// </summary>
        [Range(0, 20)] public float mouseSensitivity = 10;
        /// <summary>
        /// Цель камеры
        /// </summary>
        [SerializeField] Transform target;
        /// <summary>
        /// Параметры позиционирования камеры
        /// </summary>
        Transform cameraTransform;
        /// <summary>
        /// Дистанция до объекта слежения камеры
        /// </summary>
        [Range(0, 20)] public float distanceFromTarget;
        /// <summary>
        /// Минимальный и максимальный углы поворота камеры по Y
        /// </summary>
        public Vector2 pMinMax = new Vector2(-45, 85);
        /// <summary>
        /// Скорость сглаживания(плавности) вращения камеры
        /// </summary>
        Vector3 _rotationSmoothVelocity;
        /// <summary>
        /// Текущий поворот камеры
        /// </summary>
        Vector3 _currentRotation;
        /// <summary>
        /// Дистанция скрытия объекта
        /// </summary>
        [SerializeField] float hideDistance = 1;
        /// <summary>
        /// Дистанция фиксирования перечения луча и объекта
        /// </summary>
        [SerializeField, Range(1, 10)] float hitDistance = 1;
        /// <summary>
        /// Маска препятствий
        /// </summary>
        [SerializeField] LayerMask obstacles;
        /// <summary>
        /// Маска игрока
        /// </summary>
        [SerializeField] LayerMask noPlayer;
        /// <summary>
        /// Маска камеры
        /// </summary>
        LayerMask _camOrigin;
        /// <summary>
        /// Максимальная дистанция
        /// </summary>
        float _maxDistance;

        /// <summary>
        /// Свойство позиции камеры
        /// </summary>
        Vector3 _CameraPosition
        {
            get { return cameraTransform.position; }
            set { cameraTransform.position = value; }
        }

        private void Start()
        {
            cam = Camera.main;
            // Получение маски камеры
            _camOrigin = cam.cullingMask;
            // Получение параметра transform камеры
            cameraTransform = cam.transform;
            // Растчет макстимальной дистанйции от камеры до объекта сдележения камеры
            _maxDistance = Vector3.Distance(_CameraPosition, target.position);
        }

        /// <summary>
        /// Метод поворота камеры при помощи мыши
        /// </summary>
        /// <param name="moveMousrX"></param>
        /// <param name="moveMouseY"></param>
        public void RotateCamera(float moveMousrX, float moveMouseY, float rotationSmoothTime)
        {
            // Расчент максимального угла вращения камеры по оси Y
            moveMouseY = Mathf.Clamp(moveMouseY, pMinMax.x, pMinMax.y);

            // Расчет сглаживания движения мыши
            _currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(moveMouseY, moveMousrX), ref _rotationSmoothVelocity, rotationSmoothTime);

            // Целевой угол вращения 
            Vector3 targetRotation = _currentRotation;

            // Поворот камеры
            cameraTransform.eulerAngles = targetRotation;

            // Расчет позиции камеры
            _CameraPosition = target.position - cameraTransform.forward * distanceFromTarget;
        }

        /// <summary>
        /// Метод взаимодействия камеры с препятствиями
        /// </summary>
        public void ObstaclesReact()
        {
            // получение текущей позции камеры
            var nowDistance = Vector3.Distance(_CameraPosition, target.position);

            // Луч от цели камеры к камере
            RaycastHit hit;

            Debug.DrawRay(target.position, _CameraPosition - target.position, Color.red);

            // Установка позиции камеры при столкновении с припятствием
            if (Physics.Raycast(target.position, _CameraPosition - target.position, out hit, nowDistance, obstacles))
                _CameraPosition = hit.point + cameraTransform.forward;

            // Установка позиции камеры при удалении от препятствия
            else if (nowDistance < _maxDistance && !Physics.Raycast(_CameraPosition, -cameraTransform.forward, 0.1f, obstacles))
                _CameraPosition -= cameraTransform.forward * 0.05f;
        }

        /// <summary>
        /// Метод взаимодействия камеры с моделью игрока
        /// </summary>
        public void PlayerReact()
        {
            // Отключение видимости модели игрока при максимальном приближении камеры к модели игрока
            if (Vector3.Distance(_CameraPosition, target.position) < hideDistance)
                cam.cullingMask = noPlayer;
            // Включении модели игрока
            else
                cam.cullingMask = _camOrigin;
        }

        /// <summary>
        /// Метод получения пересечения луча и объекта
        /// </summary>
        public void GetRayHit()
        {
            // создание объекта пересечения
            RaycastHit hit;

            // Создание луча от центра камеры  
            Ray ray = new Ray(_CameraPosition, cameraTransform.forward);

            Debug.DrawRay(_CameraPosition, cameraTransform.forward, Color.green);

            // Условие пересечения камеры и объекта
            if (Physics.Raycast(ray, out hit) && Vector3.Distance(target.position, hit.transform.position) <= hitDistance)
            {
                // Получение объекта пересечения
                switch (hit.transform.tag.ToLower())
                {
                    case "door":
                        textDescription.text = "Автоматическая дверь";
                        textDescription.gameObject.SetActive(true);
                        break;
                    case "hatch":
                        textDescription.text = "(E) Открыть люк";
                        textDescription.gameObject.SetActive(true);
                        if (Input.GetButtonDown(GlobalInput.Action))
                        {
                            var temp = hit.transform.GetComponentInParent<OpenTheHatchScript>();
                            temp.OpenClose();
                        }
                        break;
                    case "button":
                        textDescription.text = "Встать для активации";
                        textDescription.gameObject.SetActive(true);
                        break;
                    case "push":
                        textDescription.text = "Отталкивает";
                        textDescription.gameObject.SetActive(true);
                        break;
                    case "damage":
                        textDescription.text = "Наносит урон";
                        textDescription.gameObject.SetActive(true);
                        break;
                    default:
                        textDescription.gameObject.SetActive(false);
                        break;
                }
            }
            else
                textDescription.gameObject.SetActive(false);
        }
    }
}
