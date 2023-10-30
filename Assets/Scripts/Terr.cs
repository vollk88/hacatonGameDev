using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

public enum TerrainType
{
    Earth = 0, //Обычная земля
    Grass = 1, //Трава
    Mud = 3, //Грязь
    Stone = 4, //Каменная плитка
    Wood = 5, //Деревянные доски
}

public class Terr : MonoBehaviour
{
    private Terrain _terr;
    private byte[,] _splatIndex;
    private Vector3 _size;
    private Vector3 _tPos;
    private int _width;
    private int _height;

    private void Start()
    {
        GameStateEvents.GameStarted += Init;
    }

    private void Init()
    {
        FindObjectOfType<CharacterController>().Terr = this;
        _terr = GetComponent<Terrain>();
        CalcHiInflPrototypeIndexesPerPoint();
    }

    private void CalcHiInflPrototypeIndexesPerPoint()
    {
        TerrainData terrainData = _terr.terrainData;
        _size = terrainData.size;
        _width = terrainData.alphamapWidth;
        _height = terrainData.alphamapHeight;
        int prototypesLength = terrainData.splatPrototypes.Length;
        _tPos = _terr.GetPosition();

        float[, ,] alphas = terrainData.GetAlphamaps(0, 0, _width, _height);
        _splatIndex = new byte[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                byte ind = 0;
                float t = 0f;
                for (byte i = 0; i < prototypesLength; i++)
                    if (alphas[x, y, i] > t)
                    {
                        t = alphas[x, y, i];
                        ind = i;
                    }
                _splatIndex[x, y] = ind;
            }
        }
    }

    public TerrainType GetMaterialIndex(Vector3 pos)
    {
        pos -= _tPos;
        pos.x /= _size.x;
        pos.z /= _size.z;
        string[] keyWords = {
            TerrainType.Earth.ToString().ToLower(),
            TerrainType.Grass.ToString().ToLower(),
            TerrainType.Mud.ToString().ToLower(),
            TerrainType.Stone.ToString().ToLower(),
            TerrainType.Wood.ToString().ToLower(),
        };
        try
        {
            if (pos.z > 1 || pos.x > 1)
                return TerrainType.Earth;
            
            string textureName = _terr.terrainData.terrainLayers[_splatIndex[(int)(pos.z * (_width - 1)), (int)(pos.x * (_height - 1))]].name.ToLower();

            foreach (string word in keyWords)
            {
                if (!textureName.Contains(word)) continue;
                
                textureName = word;
                break;
            }
            if (textureName == TerrainType.Earth.ToString().ToLower())
                return TerrainType.Earth;
            if (textureName == TerrainType.Grass.ToString().ToLower())
                return TerrainType.Grass;
            if (textureName == TerrainType.Mud.ToString().ToLower())
                return TerrainType.Mud;
            if (textureName == TerrainType.Stone.ToString().ToLower())
                return TerrainType.Stone;
            if (textureName == TerrainType.Wood.ToString().ToLower())
                return TerrainType.Wood;
            
            return TerrainType.Wood;
        }
        catch
        {
            Debug.Log("Попытка побегать по пустоте xD");
            return TerrainType.Earth;
        }
    }
}
