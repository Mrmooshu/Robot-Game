using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTableRoller : MonoBehaviour
{
    [SerializeField] public GameObject itemObject;
    [SerializeField] public List<TableAndChance> Tables;

    public void RollDrop(int xSpawnBump, int ySpawnBump)
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            int randomNum = Random.Range(0, Tables[i].chance);
            if (randomNum == 1)
            {
                GameObject newItem = GeneralManager.SpawnItem(transform, Tables[i].table.RollTable());
                newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-xSpawnBump, xSpawnBump), ySpawnBump));
            }
        }
    }
}

[System.Serializable]
public class TableAndChance
{
    public DropTable table;
    public int chance;
}
