using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;  // Singleton örneği

    Queue<Cube> cubesQueue = new Queue<Cube>();  // Kullanılabilir küplerin kuyruğu
    [SerializeField] private int cubesQueueCapacity = 20;  // Kuyruğun başlangıç kapasitesi
    [SerializeField] private bool autoQueueGrow = true;  // Kuyrukta küp kalmazsa otomatik olarak büyütme

    [SerializeField] private GameObject cubePrefab;  // Küp prefabı
    [SerializeField] private Color[] cubeColors;  // Küp renkleri

    [HideInInspector] public int maxCubeNumber;  // Maksimum küp numarası

    private int maxPower = 12;  // Maksimum güç (2^12)

    private Vector3 defaultSpawnPosition;  // Küplerin varsayılan doğma pozisyonu

    private void Awake()
    {
        Instance = this;  // Singleton örneğini ayarla

        defaultSpawnPosition = transform.position;  // Varsayılan doğma pozisyonunu ayarla
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);  // Maksimum küp numarasını hesapla

        InitializeCubesQueue();  // Küplerin kuyruğunu başlat
    }

    // Başlangıçta kuyruğa küp ekleme
    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
            AddCubeToQueue();
    }

    // Kuyruğa küp ekleme
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();

        cube.gameObject.SetActive(false);  // Küpü devre dışı bırak
        cube.IsMainCube = false;  // Ana küp olmadığını ayarla
        cubesQueue.Enqueue(cube);  // Küpü kuyruğa ekle
    }

    // Belirtilen numarada ve pozisyonda küp doğurma
    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {  // Kuyrukta küp kalmadıysa
            if (autoQueueGrow)
            {  // Kuyruk otomatik olarak büyütülecekse
                cubesQueueCapacity++;
                AddCubeToQueue();

            }
            else
            {
                Debug.LogError("[Cubes Queue] : no more cubes available in the pool");
                return null;
            }
        }

        Cube cube = cubesQueue.Dequeue();  // Kuyruktan küp al
        cube.transform.position = position;  // Küpün pozisyonunu ayarla
        cube.SetNumber(number);  // Küpün numarasını ayarla
        cube.SetColor(GetColor(number));  // Küpün rengini ayarla
        cube.gameObject.SetActive(true);  // Küpü etkinleştir

        return cube;
    }

    // Rastgele numarada küp doğurma
    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    // Küpü yok etme ve kuyruğa geri ekleme
    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;  // Küpün hızını sıfırla
        cube.CubeRigidbody.angularVelocity = Vector3.zero;  // Küpün açısal hızını sıfırla
        cube.transform.rotation = Quaternion.identity;  // Küpün rotasyonunu sıfırla
        cube.IsMainCube = false;  // Ana küp olmadığını ayarla
        cube.gameObject.SetActive(false);  // Küpü devre dışı bırak
        cubesQueue.Enqueue(cube);  // Küpü kuyruğa ekle
    }

    // Rastgele bir küp numarası oluşturma
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    // Belirtilen numaraya göre küp rengi döndürme
    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
