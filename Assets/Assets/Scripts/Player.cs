using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;  // Hareket hızı
    [SerializeField] private float pushForce;  // Fırlatma kuvveti
    [SerializeField] private float cubeMaxPosX;  // Küpün maksimum X pozisyonu
    [Space]
    [SerializeField] private TouchSlider touchSlider;  // TouchSlider referansı

    [SerializeField] private Cube mainCube;  // Ana küp referansı

    private bool isPointerDown;  // Pointer'ın basılı olup olmadığını belirten bayrak
    private bool canMove;  // Hareket edip edemeyeceğini belirten bayrak
    private Vector3 cubePos;  // Küpün pozisyonu

    private void Start()
    {
        SpawnCube();  // İlk küpü spawn et
        canMove = true;  // Başlangıçta hareket edebilir

        // TouchSlider olaylarına abone ol
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        // Pointer basılıysa küpü hareket ettir
        if (isPointerDown)
            mainCube.transform.position = Vector3.Lerp(
               mainCube.transform.position,
               cubePos,
               moveSpeed * Time.deltaTime
            );
    }

    private void OnPointerDown()
    {
        isPointerDown = true;  // Pointer basılı
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;  // Mevcut küp pozisyonunu al
            cubePos.x = xMovement * cubeMaxPosX;  // Yeni X pozisyonunu ayarla
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;  // Pointer artık basılı değil
            canMove = false;  // Hareket artık mümkün değil

            // Küpü fırlat
            mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

            Invoke("SpawnNewCube", 0.3f);  // Yeni bir küp spawn et
        }
    }

    private void SpawnNewCube()
    {
        mainCube.IsMainCube = false;  // Eski ana küp artık ana küp değil
        canMove = true;  // Hareket tekrar mümkün
        SpawnCube();  // Yeni bir küp spawn et
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();  // Rastgele bir küp spawn et
        mainCube.IsMainCube = true;  // Yeni küp ana küp

        cubePos = mainCube.transform.position;  // Yeni küp pozisyonunu sıfırla
    }

    private void OnDestroy()
    {
        // TouchSlider olay aboneliklerini kaldır
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
