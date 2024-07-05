using UnityEngine;
using TMPro;  // TextMeshPro kullanımı için gerekli

public class Cube : MonoBehaviour
{
    static int staticID = 0;  // Tüm Cube nesneleri için benzersiz bir kimlik sağlamak amacıyla kullanılan statik değişken
    [SerializeField] private TMP_Text[] numbersText;  // Küp yüzeylerindeki numaraları göstermek için TextMeshPro referansları

    [HideInInspector] public int CubeID;  // Her küp için benzersiz kimlik
    [HideInInspector] public Color CubeColor;  // Küpün rengi
    [HideInInspector] public int CubeNumber;  // Küpün numarası
    [HideInInspector] public Rigidbody CubeRigidbody;  // Küpün Rigidbody bileşeni
    [HideInInspector] public bool IsMainCube;  // Küpün ana küp(ana küp firlattigim) olup olmadığını belirten bayrak

    private MeshRenderer cubeMeshRenderer;  // Küpün mesh renderer bileşeni

    private void Awake()
    {
        CubeID = staticID++;  // Benzersiz kimlik ataması
        cubeMeshRenderer = GetComponent<MeshRenderer>();  // MeshRenderer bileşenini al
        CubeRigidbody = GetComponent<Rigidbody>();  // Rigidbody bileşenini al
    }

    public void SetColor(Color color)
    {
        CubeColor = color;  // Renk değişkenini ayarla
        cubeMeshRenderer.material.color = color;  // Küpün materyal rengini ayarla
        
    }

    public void SetNumber(int number)
    {
        CubeNumber = number;  // Numara değişkenini ayarla
        for (int i = 0; i < 6; i++)
        {  // Küpün tüm yüzlerindeki numaraları ayarla
            numbersText[i].text = number.ToString();  // Her TextMeshPro bileşenine numarayı yaz
        }
    }
}
