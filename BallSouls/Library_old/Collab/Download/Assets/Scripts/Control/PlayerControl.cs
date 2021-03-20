using UnityEngine;

namespace Homewor11.InputControl
{
    [RequireComponent(typeof(Rigidbody))]
    class PlayerControl : MonoBehaviour
    {
        /// <summary>
        /// Экзкмпляр твердого тела модели игрока
        /// </summary>
        private Rigidbody playerRigitBody;

        /// <summary>
        ///  Параметры позиционирования цели слежения камеры
        /// </summary>
        [SerializeField] Transform cameraTarget;

        /// <summary>
        /// Параметр скорости игрока
        /// </summary>
        [SerializeField, Range(0, 20)] private float playerSpeed = 10;

        /// <summary>
        /// Параметр высоты прыжка игрока
        /// </summary>
        [SerializeField, Range(2f, 10)] private float playerJump = 5;

        /// <summary>
        /// Высота камеры
        /// </summary>
        [Range(0, 1)] public float cameraYPosition;

        /// <summary>
        /// Скорость сглаживания поворота камеры
        /// </summary>
        float turnSmoothVelocity = 0.12f;

        /// <summary>
        /// Свойство текущего положения цели камеры 
        /// </summary>
        Vector3 _nowCameraTargetPosition
        {
            get { return cameraTarget.position; }
            set { cameraTarget.position = value; }
        }

        /// <summary>
        /// Свойство текущего положения цели камеры 
        /// </summary>
        Quaternion _nowCameraTargetRotation
        {
            get { return cameraTarget.rotation; }
            set { cameraTarget.rotation = value; }
        }

        /// <summary>
        /// Свойство текущего положения модели игрока (для позиционирования цели камеры)
        /// </summary>
        Vector3 _nowObjectPosition
        {
            get { return new Vector3(transform.position.x, transform.position.y + cameraYPosition, transform.position.z); }
        }
       
        void Start()
        {
            // Получение твердого тела модели игрока
            playerRigitBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Установка позиции цели камеры для предотвращения смещения цели камеры и камеры относительно моели игрока
            _nowCameraTargetPosition = _nowObjectPosition;           
        }

        /// <summary>
        /// Метод перемещения игрока
        /// </summary>
        /// <param name="move"></param>
        public void MoveCharacter(Vector3 move, Camera cameraLook)
        {
            #region Перемещение игрока на основе физической модели
            if (move.z > 0)
                playerRigitBody.AddForce(cameraLook.transform.forward * playerSpeed);
            if (move.z < 0)
                playerRigitBody.AddForce(-cameraLook.transform.forward * playerSpeed);
            if (move.x > 0)
                playerRigitBody.AddForce(cameraLook.transform.right * playerSpeed);
            if (move.x < 0)
                playerRigitBody.AddForce(-cameraLook.transform.right * playerSpeed);
            #endregion

            // Перемещение модели игрока на основеве изменения положения модели в пространстве
            //playerRigitBody.transform.Translate(move * playerSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Метод поворота модели игрока в соответствии с тем куда смотрит камера (перегрузка с принимаемыми параметрами угла вращения камеры и времени сглаживания поворота камеры)
        /// </summary>
        /// <param name="rotate"></param>
        /// <param name="turnSmoothTime"></param>
        public void RotateCharacter(float rotate, float turnSmoothTime)
        {
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotate, ref turnSmoothVelocity, turnSmoothTime);
        }

        /// <summary>
        /// Метод прыжка игрока
        /// </summary>
        /// <param name="jump"></param>
        public void JumpCharacter(Vector3 jump)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.2f))
                playerRigitBody.AddForce(jump * playerJump, ForceMode.Impulse);
        }       
    }
}
