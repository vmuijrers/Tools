using UnityEngine;

public class PlaceObjectCommand : Command
{
    private GameObject spawnedObject;
    private GameObject objectToSpawn;
    private Vector3 position;
    private Quaternion rotation;

    public PlaceObjectCommand(GameObject spawnObject, Vector3 position, Quaternion rotation)
    {
        objectToSpawn = spawnObject;
        this.position = position;
        this.rotation = rotation;
    }

    public override void Execute()
    {
        spawnedObject = Object.Instantiate(objectToSpawn, position, rotation);
    }

    public override void Undo()
    {
        if(spawnedObject != null)
        {
            Object.Destroy(spawnedObject);
        }
    }
}
