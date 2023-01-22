using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    public AssetBundle generalPrefabs;
    public AssetBundle variantPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            generalPrefabs = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "general prefabs"));
            variantPrefabs = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variant prefabs"));
        }
    }

    public static GameObject SpawnItem(Transform transform, Item item)
    {
        GameObject itemObject = Instantiate(instance.generalPrefabs.LoadAsset<GameObject>("ItemObject"), transform.position, new Quaternion());
        itemObject.GetComponent<ItemObject>().SetItem(item);
        return itemObject;
    }
}
