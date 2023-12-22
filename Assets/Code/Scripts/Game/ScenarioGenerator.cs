using System.Collections;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour {
    public ScenarioTile[] wallTiles;
    public ScenarioTile[] centerTiles;
    public float probabilityOfCenterTile;

    public int initialTilesAmount;
    public float bottomYPosition;
    public float topYPosition;

    private Vector3 nextTopTilePosition;
    private Vector3 nextBottomTilePosition;

    private int totalWeight;

    [SerializeField] private Material[] tileMaterials;

    void Start() {
        CalculateTotalWeight();
        AdjustDifficult(level: 0);
        nextTopTilePosition = new Vector3(0, topYPosition, 0);
        nextBottomTilePosition = new Vector3(0, bottomYPosition, 0);
        GenerateInitialTiles();
        StartCoroutine(GenerateCenterTile());

    }
    private void Update() {
        int bot = GameObject.FindGameObjectsWithTag("Tile").Length;
        int top = GameObject.FindGameObjectsWithTag("TileUp").Length;
        int center = GameObject.FindGameObjectsWithTag("TileCenter").Length;
        Debug.Log("Tiles:" + (bot + top + center) + "Top: " + top + "Bot: " + bot + "Center: " + center);
    }

    // Calcula el peso total de todos los tiles disponibles para la generación
    void CalculateTotalWeight() {
        totalWeight = 0;
        foreach (ScenarioTile tile in wallTiles) {
            totalWeight += tile.weight;
        }
    }

    // Genera una cantidad inicial de tiles al comienzo
    void GenerateInitialTiles() {
        for (int i = 0; i < initialTilesAmount; i++) {
            GenerateTopTile();
            GenerateBottomTile();
        }
    }

    // Genera y posiciona un nuevo tile en el escenario
    public void GenerateTopTile() {
        GameObject topTilePrefab = SelectTilePrefab(wallTiles);

        // Instanciar el segmento superior
        GameObject topTile = Instantiate(topTilePrefab, nextTopTilePosition, Quaternion.identity);
        topTile.transform.localScale = new Vector3(1, -1, 1);
        topTile.tag = topTilePrefab.tag + "Up";

        // Ajustar la posición del próximo segmento
        AdjustNextSegmentPosition(topTile);
    }

    public void GenerateBottomTile() {
        GameObject bottomTilePrefab = SelectTilePrefab(wallTiles);

        // Instanciar el segmento inferior
        GameObject bottomTile = Instantiate(bottomTilePrefab, nextBottomTilePosition, Quaternion.identity);
        // Ajustar la posición del próximo segmento
        AdjustNextSegmentPosition(bottomTile);
    }
    private void AdjustNextSegmentPosition(GameObject tile) {
        Renderer renderer = tile.GetComponent<Renderer>();
        if (renderer != null) {
            // Usa el tamaño del collider para determinar la longitud del tile
            if (tile.CompareTag("Tile")) {
                nextBottomTilePosition.x += renderer.bounds.size.x;
            } else {
                nextTopTilePosition.x += renderer.bounds.size.x;
            }
        }
    }
    public IEnumerator GenerateCenterTile() {
        do {
            yield return new WaitForSeconds(1f);

            // Decidir si generar un tile central
            if (Random.value < probabilityOfCenterTile) {
                GameObject centerTilePrefab = SelectTilePrefab(centerTiles);
                Vector3 centerPosition = new Vector3(nextTopTilePosition.x, 0, nextTopTilePosition.z);
                GameObject centerTile = Instantiate(centerTilePrefab, centerPosition, Quaternion.identity);
            }
        }
        while (GameManager.Instance.isGameRunning);
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
