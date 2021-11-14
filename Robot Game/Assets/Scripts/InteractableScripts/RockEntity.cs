using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockEntity : InteractableEntity
{
    [SerializeField] public GameObject itemObject;
    [SerializeField] public DropTable oreDropTable;
    [SerializeField] public int oreTableChance;
    [SerializeField] public DropTable rockDropTable;
    [SerializeField] public int rockTableChance;
    [SerializeField] public DropTable gemDropTable;
    [SerializeField] public int gemTableChance;
    [SerializeField] public DropTable luckDropTable;
    [SerializeField] public int luckTableChance;
    
    private Button mineButton;
    private GolemEntity player;

    public override void Start()
    {
        base.Start();
        mineButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        mineButton.gameObject.SetActive(false);

        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<GolemEntity>() != null)
            {
                player = collision.GetComponent<GolemEntity>();
                mineButton.gameObject.SetActive(true);
                mineButton.onClick.AddListener(player.ToggleMining);
                player.currentRock = this;
            }
            else
            {

            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<GolemEntity>() != null)
            {
                mineButton.gameObject.SetActive(false);
                mineButton.onClick.RemoveListener(player.ToggleMining);
                player.currentRock = null;
            }
            else
            {

            }
        }
    }

    public void RollDrop()
    {
        int randomNum = Random.Range(0, oreTableChance);
        if (randomNum <= 1)//+mining
        {
            GameObject newItem = Instantiate(itemObject, transform);
            newItem.GetComponent<ItemObject>().SetItem(oreDropTable.RollTable());
        }
        randomNum = Random.Range(0, rockTableChance);
        if (randomNum <= 1)//+mining
        {
            GameObject newItem = Instantiate(itemObject, transform);
            newItem.GetComponent<ItemObject>().SetItem(rockDropTable.RollTable());
        }
        randomNum = Random.Range(0, gemTableChance);
        if (randomNum <= 1)//+gem chance
        {
            GameObject newItem = Instantiate(itemObject, transform);
            newItem.GetComponent<ItemObject>().SetItem(gemDropTable.RollTable());
        }
        randomNum = Random.Range(0, luckTableChance);
        if (randomNum <= 1)//+luck
        {
            GameObject newItem = Instantiate(itemObject, transform);
            newItem.GetComponent<ItemObject>().SetItem(luckDropTable.RollTable());
        }

    }
}
