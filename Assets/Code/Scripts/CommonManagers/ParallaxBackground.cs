using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public Renderer movingLayer1;    // Primera capa móvil
    public Renderer movingLayer2;    // Segunda capa móvil
    public float speed1 = 0.5f;      // Velocidad de desplazamiento para la primera capa móvil
    public float speed2 = 0.3f;      // Velocidad de desplazamiento para la segunda capa móvil

    private Vector2 savedOffset1;    // Offset inicial para la primera capa móvil
    private Vector2 savedOffset2;    // Offset inicial para la segunda capa móvil

    void Start() {
        // Guardar los offsets iniciales
        savedOffset1 = movingLayer1.sharedMaterial.GetTextureOffset("_MainTex");
        savedOffset2 = movingLayer2.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update() {
        if (!GameManager.Instance.isGameRunning) {
            return;
        }
        // Calcular el nuevo offset
        float newOffsetX1 = Time.time * speed1;
        float newOffsetX2 = Time.time * speed2;

        // Aplicar el offset a las capas móviles
        movingLayer1.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(newOffsetX1, savedOffset1.y));
        movingLayer2.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(newOffsetX2, savedOffset2.y));
    }

    void OnDisable() {
        // Restaurar los offsets iniciales al desactivar
        movingLayer1.sharedMaterial.SetTextureOffset("_MainTex", savedOffset1);
        movingLayer2.sharedMaterial.SetTextureOffset("_MainTex", savedOffset2);
    }
}
