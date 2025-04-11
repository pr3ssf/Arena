using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    [Header("Целевые позиции камеры")]
    public Transform CamPos;         // Основная позиция камеры
    public Transform PlayerCamPos;   // Позиция камеры для игрока
    public Transform EnemyCamPos;    // Позиция камеры для врага

    [Header("Настройки плавного перехода")]
    [Tooltip("Время плавного перехода позиции")]
    public float positionSmoothTime = 0.3f;
    [Tooltip("Время плавного перехода поворота")]
    public float rotationSmoothTime = 0.3f;

    // Текущая цель, к которой движется камера
    private Transform targetTransform;
    // Переменная для SmoothDamp
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // При запуске плеймода устанавливаем камеру в позицию CamPos
        if (CamPos != null)
        {
            targetTransform = CamPos;
            transform.position = CamPos.position;
            transform.rotation = CamPos.rotation;
        }
    }

    void Update()
    {
        // Обработка нажатия клавиш: 1 - CamPos, 2 - PlayerCamPos, 3 - EnemyCamPos
        if (Input.GetKeyDown(KeyCode.Alpha1) && CamPos != null)
        {
            targetTransform = CamPos;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerCamPos != null)
        {
            targetTransform = PlayerCamPos;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && EnemyCamPos != null)
        {
            targetTransform = EnemyCamPos;
        }

        // Если выбрана цель, осуществляем плавный переход камеры
        if (targetTransform != null)
        {
            // Плавное перемещение позиции
            transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, positionSmoothTime);
            // Плавное изменение поворота
            transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, Time.deltaTime / rotationSmoothTime);
        }
    }
}
