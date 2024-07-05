using UnityEngine ;

public class RedZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // Diğer nesneden Cube bileşenini alır.
        Cube cube = other.GetComponent<Cube>();

        // Cube bileşeninin mevcut olup olmadığını kontrol eder.
        if (cube != null)
        {
            // Cube'nin ana küp olmadığını ve hızının 0.1'den az olduğunu kontrol eder.
            if (!cube.IsMainCube && cube.CubeRigidbody != null && cube.CubeRigidbody.velocity.magnitude < 0.1f)
            {
                Debug.Log("Gameover"); // Koşullar sağlanıyorsa "Gameover" mesajını yazdırır.
            }
        }
    }

}
