using System;
using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using Inventory;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Requrements
{
    OnSwamp = 1,
    NearWater = 2,
    NearWood = 3,
    OnGarden = 4,
}
public class SpawnItems : CustomBehaviour
{
    [Serializable]
    class SpawnRules
    {
        public EItems item;
        public Requrements requrements;
        [Range(0, 1)] public float ratio = 1f;
    }
    [SerializeField] private List<SpawnRules> _spawnRequrementsList;
    Terrain terr;


    private void Start()
    {
        terr = GetComponent<Terrain>();
        SpawnItemsNearTrees();
    }

    
    private Vector3[] GetWoodsPositions()
    {
        var res = new List<Vector3>();
        var trees = terr.terrainData.treeInstances;

        foreach (var elem in trees)
        {
            var pos = Vector3.Scale(elem.position, terr.terrainData.size) + terr.transform.position;
            res.Add(pos);
        }
        return res.ToArray();
    }
    
    private void SpawnItemsNearTrees()
    {
        StartCoroutine(SpawnItemss());
    }
    

    private SpawnRules[] GetMathingObj(Requrements requrements)
    {
        var res = new List<SpawnRules>();
        Debug.Log("Requested Requirement: " + requrements);
        foreach (var obj in _spawnRequrementsList)
        {
            Debug.Log("Item Requirement: " + obj.requrements);
            if (obj.requrements == requrements)
            {
                Debug.Log("Matching item found.");
                res.Add(obj);
            }
        }
    
        return res.ToArray();
    }

    private IEnumerator SpawnItemss()
    {
        Vector3[] treePositions = GetWoodsPositions();
        SpawnRules[] wooditems = GetMathingObj(Requrements.NearWood);
        foreach (var item in wooditems)
        {
            GameObject spawnobj = InventoryController.GetItemFromType(item.item);
            Debug.Log("spawnobj");
            foreach (Vector3 treePosition in treePositions)
            {
                if (Random.Range(0f, 1f) > item.ratio)
                {
                    // Debug.Log("skip");
                    continue;
                }
                yield return new WaitForFixedUpdate();
                // Генерируйте случайную позицию в радиусе 5 метров от дерева.
                Vector2 randomOffset = Random.insideUnitSphere * 5f;
                Vector3 spawnPosition = treePosition + new Vector3(randomOffset.x, 0f, randomOffset.y);

                spawnPosition.y = terr.SampleHeight(spawnPosition);

                // Создайте и спавните ваш предмет на полученной позиции.
                var obj = Instantiate(spawnobj, spawnPosition, Quaternion.identity);
                var rb = obj.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            }
        }
    }

}
