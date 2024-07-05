using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;  // Küp patlama efekti

    ParticleSystem.MainModule cubeExplosionFXMainModule;  // Patlama efektinin ana modülü

    public static FX Instance;  // Singleton örneği

    private void Awake()
    {
        Instance = this;  // Singleton örneğini ayarla
    }

    private void Start()
    {
        cubeExplosionFXMainModule = cubeExplosionFX.main;  // Ana modülü al
    }

    // Patlama efektini oynat
    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        Color newColor = new Color(color.r, color.g, color.b, 1);
        
        // Patlama rengini ayarla
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(newColor);
        cubeExplosionFX.transform.position = position;  // Patlama pozisyonunu ayarla
        cubeExplosionFX.Play();  // Patlama efektini oynat
    }
}
