using UnityEngine; 
using UnityEngine.Events;  
using UnityEngine.EventSystems; 
using UnityEngine.UI;  

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // UnityAction olayları dışarıdan atanabilir fonksiyonlar
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    // Slider bileşenine referans
    private Slider uiSlider;

    // Awake metodu script yüklendiğinde çalışır
    private void Awake()
    {
        // slider componenti
        uiSlider = GetComponent<Slider>();

        // Slider'ın değer değişim olayına bir dinleyici ekle
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Pointer (dokunmatik veya fare) aşağıya bastığında tetiklenir
    public void OnPointerDown(PointerEventData eventData)
    {
        // OnPointerDownEvent olayını tetikle (abone olan fonksiyon varsa)
        if (OnPointerDownEvent != null)
            OnPointerDownEvent.Invoke();

        // OnPointerDragEvent olayını tetikle ve Slider'ın mevcut değerini gönder
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(uiSlider.value);
    }

    // Slider'ın değeri değiştiğinde tetiklenir
    private void OnSliderValueChanged(float value)
    {
        // OnPointerDragEvent olayını tetikle ve yeni değeri gönder
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(value);
    }

    // Pointer (dokunmatik veya fare) yukarı kaldırıldığında tetiklenir
    public void OnPointerUp(PointerEventData eventData)
    {
        // OnPointerUpEvent olayını tetikle (abone olan fonksiyon varsa)
        if (OnPointerUpEvent != null)
            OnPointerUpEvent.Invoke();

        // Slider'ın değerini sıfırla
        uiSlider.value = 0f;
    }

    // Script yok edildiğinde çağrılır
    private void OnDestroy()
    {
        //listener bir olay gerceklestiginde belirli bi islevin otomatik olarak cagrilmasini saglar
        // Dinleyiciyi kaldır (bellek sızıntılarını önlemek için)
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
