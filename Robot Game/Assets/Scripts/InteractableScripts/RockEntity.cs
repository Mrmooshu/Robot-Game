using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockEntity : InteractableEntity
{
    [SerializeField] private Button mineButton;
    [SerializeField] private Button assignButton;

    private GolemEntity player;

    public override void Start()
    {
        base.Start();
        mineButton.gameObject.SetActive(false);
        assignButton.gameObject.SetActive(false);

        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<GolemEntity>() != null)
            {
                player = collision.GetComponent<GolemEntity>();
                mineButton.gameObject.SetActive(true);
                assignButton.gameObject.SetActive(true);
                mineButton.onClick.AddListener(player.StartMining);
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
                assignButton.gameObject.SetActive(false);
                mineButton.onClick.RemoveListener(player.StartMining);
            }
            else
            {

            }
        }
    }
}
