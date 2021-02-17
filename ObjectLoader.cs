using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ObjectLoader", menuName = "CommandPattern/ObjectLoader")]
public class ObjectLoader : ScriptableObject
{
    [SerializeField] private string FolderName;
    private List<GameObject> objectList;
    private List<GameObject> visualList;
    private int itemIndex = 0;

    public void LoadObjects()
    {
        visualList = new List<GameObject>();
        objectList = new List<GameObject>();
        objectList = Resources.LoadAll<GameObject>(FolderName).ToList();
        for (int i = 0; i < objectList.Count; i++)
        {
            visualList.Add(Instantiate(objectList[i]));
        }
        visualList.ForEach(x =>
        {
            x.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(y => y.material.color = Color.green);
            x.GetComponentsInChildren<Collider>().ToList().ForEach(y => y.enabled = false);
            x.SetActive(false);
        });
    }

    public GameObject GetCurrentVisual()
    {
        return visualList[itemIndex];
    }

    public GameObject GetNextObject()
    {
        visualList[itemIndex]?.SetActive(false);
        itemIndex++;
        if (itemIndex >= objectList.Count)
        {
            itemIndex = 0;
        }
        visualList[itemIndex]?.SetActive(true);
        return objectList[itemIndex];
    }

    public GameObject GetPreviousObject()
    {
        visualList[itemIndex]?.SetActive(false);
        itemIndex--;
        if (itemIndex < 0)
        {
            itemIndex = objectList.Count - 1;
        }
        visualList[itemIndex]?.SetActive(true);
        return objectList[itemIndex];
    }
}
