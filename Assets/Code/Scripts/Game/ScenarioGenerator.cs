using System.Collections;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour {
    public ScenarioTile[] wallTiles;
    public ScenarioTile[] centerTiles;
    public float probabilityOfCenterTile;

    public int initialTilesAmount;
    public float bottomYPosition;
    public float topYPosition;

    private Vector3 nextWallPosition;

    private int totalWeight;

    [SerializeField] private Material[] tileMaterials;

    void Start() {
        CalculateTotalWeight();
        nextWallPosition = new Vector3(0, bottomYPosition, 0);
        GenerateInitialTiles();
        StartCoroutine(GenerateCenterTile());
    }


    // Calcula el peso total de todos los tiles disponibles para la generación
    void CalculateTotalWeight() {
        totalWeight = 0;
        foreach (ScenarioTile tile in wallTiles) {
            totalWeight += tile.weight;
        }
    }

    private void Update() {
        int bot = GameObject.FindGameObjectsWithTag("Tile").Length;
        int up = GameObject.FindGameObjectsWithTag("Tile").Length;
        Debug.Log(bot + up);
    }
    // Genera una cantidad inicial de tiles al comienzo
    void GenerateInitialTiles() {
        for (int i = 0; i < initialTilesAmount; i++) {
            GenerateTile();
        }
    }

    // Genera y posiciona un nuevo tile en el escenario
    public void GenerateTile() {
        // TODO: separar el top del bottom
        GameObject topTilePrefab = SelectTilePrefab(wallTiles);
        GameObject bottomTilePrefab = SelectTilePrefab(wallTiles);

        // Instanciar el segmento inferior
        GameObject bottomSegment = Instantiate(bottomTilePrefab, nextWallPosition, Quaternion.identity);

        // Instanciar el segmento superior
        Vector3 topPosition = new Vector3(nextWallPosition.x, topYPosition, nextWallPosition.z);
        GameObject topSegment = Instantiate(topTilePrefab, topPosition, Quaternion.identity);
        topSegment.tag = topTilePrefab.tag + "Up";
        topSegment.transform.localScale = new Vector3(topSegment.transform.localScale.x, -1, topSegment.transform.localScale.z);

        // Ajustar la posición del próximo segmento
        AdjustNextSegmentPosition(bottomSegment);
    }

    public IEnumerator GenerateCenterTile() {
        do {
            yield return new WaitForSeconds(1f);

            // Decidir si generar un tile central
            if (Random.value < probabilityOfCenterTile) {
                GameObject centerTilePrefab = SelectTilePrefab(centerTiles);
                Vector3 centerPosition = new Vector3(nextWallPosition.x, 0, nextWallPosition.z);
                GameObject centerTile = Instantiate(centerTilePrefab, centerPosition, Quaternion.identity);
            }
        }
        while (GameManager.Instance.isGameRunning);
    }

    private void AdjustNextSegmentPosition(GameObject segment) {
        Renderer renderer = segment.GetComponent<Renderer>();
        if (renderer != null) {
            // Usa el tamaño del collider para determinar la longitud del tile
            nextWallPosition.x += renderer.bounds.size.x;
        }
    }

    // Selecciona un tile basado en el peso para su generación
    GameObject SelectTilePrefab(ScenarioTile[] tiles) {
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (ScenarioTile tile in tiles) {
            currentWeight += tile.weight;
            if (randomWeight < currentWeight) {
                return tile.gameObject;
            }
        }

        // Retorna el último tile por defecto si algo sale mal
        return tiles[tiles.Length - 1].gameObject;
    }



    // Función para ajustar los pesos
    public void AdjustTileWeights(string tileName, int newWeight) {
        foreach (var tile in wallTiles) {
            if (tile.gameObject.name == tileName) {
                tile.weight = newWeight;
                break;
            }
        }
        CalculateTotalWeight(); // Recalcular el peso total después de hacer el ajuste
    }

    private void AdjustTileMaterials(int level) {
        foreach (var tile in wallTiles) {
            tile.gameObject.GetComponent<Renderer>().material = tileMaterials[level];
        }
        foreach (var tile in centerTiles) {
            tile.gameObject.GetComponent<Renderer>().material = tileMaterials[level];
        }
    }

    public void AdjustDifficult(int level) {
        AdjustTileMaterials(level);
        switch (level) {
            case 0:
                AdjustTileWeights("tilePlain1", 200);
                AdjustTileWeights("tilePlain2", 30);
                AdjustTileWeights("tileEmpty", 60);
                AdjustTileWeights("tilePointed1", 15);
                AdjustTileWeights("tilePointed2", 10);
                AdjustTileWeights("tilePointed3", 3);
                break;
            case 1:
                AdjustTileWeights("tilePlain1", 120);
                AdjustTileWeights("tilePlain2", 50);
                AdjustTileWeights("tileEmpty", 15);
                AdjustTileWeights("tilePointed1", 45);
                AdjustTileWeights("tilePointed2", 30);
                AdjustTileWeights("tilePointed3", 15);
                break;
            case 2:
                AdjustTileWeights("tilePlain1", 80);
                AdjustTileWeights("tilePlain2", 50);
                AdjustTileWeights("tileEmpty", 15);
                AdjustTileWeights("tilePointed1", 30);
                AdjustTileWeights("tilePointed2", 30);
                AdjustTileWeights("tilePointed3", 30);
                break;
            case 3:
                AdjustTileWeights("tilePlain1", 30);
                AdjustTileWeights("tilePlain2", 50);
                AdjustTileWeights("tileEmpty", 15);
                AdjustTileWeights("tilePointed1", 10);
                AdjustTileWeights("tilePointed2", 30);
                AdjustTileWeights("tilePointed3", 30);
                break;
        }
    }
}
