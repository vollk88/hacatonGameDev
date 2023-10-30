using System;
using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using Inventory;
using Items;
using Unity.VisualScripting;
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
    [SerializeField] private SphereCollider SwampArea;
    [SerializeField] private GameObject patrolPoint;
    [SerializeField] private GameObject ghostSpawner;
    [SerializeField] private GameObject leshiySpawner;


    private void Start()
    {
        terr = GetComponent<Terrain>();
        SpawnItemsNearTrees();
        StartCoroutine(SpawnSwampItems());
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
        StartCoroutine(SpawnWoodItems());
        StartCoroutine(SpawnWoodSpawners());
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
    
    private IEnumerator SpawnWoodSpawners()
    {
        Vector3[] treePositions = GetWoodsPositions();
        GameObject spawnobj = ghostSpawner;
        foreach (Vector3 treePosition in treePositions)
        {
            if (Random.Range(0f, 1f) > 0.02f)
                continue;

            yield return new WaitForFixedUpdate();
            // Генерируйте случайную позицию в радиусе 5 метров от дерева.
            Vector2 randomOffset = Random.insideUnitSphere * 5f;
            Vector3 spawnPosition = treePosition + new Vector3(randomOffset.x, 0f, randomOffset.y);

            spawnPosition.y = terr.SampleHeight(spawnPosition);

            // Создайте и спавните ваш предмет на полученной позиции.
            Instantiate(spawnobj, spawnPosition, Quaternion.identity);
            randomOffset *= 20;
            spawnPosition += new Vector3(randomOffset.x, 0f, randomOffset.y);
            spawnPosition.y = terr.SampleHeight(spawnPosition);
            Instantiate(patrolPoint, spawnPosition, Quaternion.identity);
        }
    }
    

    private IEnumerator SpawnWoodItems()
    {
        Vector3[] treePositions = GetWoodsPositions();
        SpawnRules[] wooditems = GetMathingObj(Requrements.NearWood);
        foreach (var item in wooditems)
        {
            GameObject spawnobj = InventoryController.GetItemFromType(item.item);
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
                SpawnItem(spawnobj, spawnPosition);
                
            }
        }
    }

    private IEnumerator SpawnSwampItems()
    {
        float radius = SwampArea.radius;
        SpawnRules[] swampitems = GetMathingObj(Requrements.OnSwamp);

        foreach (var item in swampitems)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Random.Range(0f, 1f) > item.ratio)
                {
                    i--;
                    continue;
                }
                Vector3 randPos = Random.onUnitSphere * radius + SwampArea.transform.position;
                randPos.y = terr.SampleHeight(randPos);
                if (Physics.Raycast(randPos, Vector3.down * 5, out RaycastHit hit, 8, LayerMask.GetMask("Water")))
                {
                    SpawnItem(InventoryController.GetItemFromType(item.item), randPos);
                }
            }
        }
        //Spawn items on swamp near water
        yield return null;
    }


    void SpawnItem(GameObject item, Vector3 pos, bool isrb = true)
    {
        GameObject obj = null;
        try
        {
            obj = Instantiate(item, pos, Quaternion.identity);
        }
        catch
        {
            return;
        }
        if (!isrb) return;
        var rb = obj.GetComponent<Rigidbody>();
        if (rb is null) return;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.Sleep();
    }
}
