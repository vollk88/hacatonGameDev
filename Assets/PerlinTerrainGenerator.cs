using UnityEngine;

public class PerlinTerrainGenerator : MonoBehaviour
{
    public Terrain terrain; // Перетащите ваш Terrain объект сцены сюда
    public float heightScale = 30.0f;
    public float noiseScale = 0.1f;

    public int numAreas = 5; // Количество случайных областей
    public float targetHeight = 0.0f; // Желаемая высота
    public float heightVariation = 3.0f; // Вариация высоты
    public int areaSize = 10; // Размер каждой области
    public float flatHeight = 10.0f; // Высота области
    public GameObject Spawner;

    private void OnValidate()
    {
        terrain = GetComponent<Terrain>();

    }


    void SetSpawners()
    {
        
    }

    void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Генерируем высоты на основе шума Перлина
                float xCoord = (float)x / width * noiseScale;
                float yCoord = (float)y / height * noiseScale;
                float heightValue = Mathf.PerlinNoise(xCoord, yCoord) * heightScale;
                heights[x, y] = heightValue;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    void FlattenRandomAreas()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        for (int i = 0; i < numAreas; i++)
        {
            int areaSize = Random.Range(this.areaSize/2, this.areaSize*2);
            int startX = Random.Range(0, width - areaSize);
            int startZ = Random.Range(0, height - areaSize);

            for (int x = startX; x < startX + areaSize; x++)
            {
                for (int z = startZ; z < startZ + areaSize; z++)
                {
                    if (x >= 0 && x < width && z >= 0 && z < height)
                    {
                        float currentHeight = heights[x, z];
                        heights[x, z] = Mathf.Clamp(currentHeight, targetHeight - heightVariation, targetHeight + heightVariation);
                    }
                }
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}
