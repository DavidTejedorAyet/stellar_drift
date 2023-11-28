using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AM_VolumeSlider : MonoBehaviour {

    [Header("References")]
    [SerializeField] private TextMeshProUGUI percentageLabel;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> onValueChange;
    private Slider slider;

    void Awake() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeValue);
    }

    void Start() {
        slider.value = 1f;
        ChangeValue(1f);
    }

    private void ChangeValue(float value) {
        onValueChange?.Invoke(value);
        percentageLabel.text = $"{(value * 100):F0}%";
    }
}
