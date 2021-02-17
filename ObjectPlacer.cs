using UnityEngine;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private ObjectLoader objectHolder;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float rayCastRange;

    private List<Command> commandList = new List<Command>();
    private GameObject currentItem;
    private int commandIndex = -1;
    private float snapAmount = 2;
    private float snapDif = 0.5f;
    private bool snapToGrid;

    // Start is called before the first frame update
    private void Start()
    {
        objectHolder = Instantiate(objectHolder);
        objectHolder.LoadObjects();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            currentItem = objectHolder.GetNextObject();
        }
        if (scroll < 0)
        {
            currentItem = objectHolder.GetPreviousObject();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            snapAmount += snapDif;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            snapAmount -= snapDif;
        }
        snapAmount = Mathf.Max(snapAmount, snapDif);

        if (Input.GetKeyDown(KeyCode.R))
        {
            snapToGrid = !snapToGrid;
        }

        RaycastHit hit;
        if (DoRaycastFromCamera(out hit))
        {
            Vector3 pos = hit.point;
            if(snapToGrid)
            {
                pos = SnapToGrid(pos, snapAmount);
            }

            ShowVisual(objectHolder.GetCurrentVisual(), pos, transform.rotation);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentItem != null)
                {
                    Command command = new PlaceObjectCommand(currentItem, pos, transform.rotation);
                    command.Execute();
                    commandIndex++;
                    commandList.Insert(commandIndex, command);
                }
            }
        }

        //Undo
        if (Input.GetMouseButtonDown(1))
        {
            if(commandIndex >= 0)
            {
                commandList[commandIndex].Undo();
                commandIndex--;
            }
        }

        //Redo
        if (Input.GetMouseButtonDown(2))
        {
            if(commandIndex < commandList.Count - 1)
            {
                commandIndex++;
                commandList[commandIndex].Execute();
            }
        }
    }

    private Vector3 SnapToGrid(Vector3 worldPosition, float roundAmount)
    {
        int valX = Mathf.RoundToInt(worldPosition.x / roundAmount);
        int valY = Mathf.RoundToInt(worldPosition.y / roundAmount);
        int valZ = Mathf.RoundToInt(worldPosition.z / roundAmount);
        return new Vector3(valX * roundAmount, valY * roundAmount, valZ * roundAmount);
    }

    private void ShowVisual(GameObject visualObject, Vector3 position, Quaternion rotation)
    {
        if(visualObject == null) { return; }
        visualObject.transform.position = position;
        visualObject.transform.rotation = rotation;
    }

    private bool DoRaycastFromCamera(out RaycastHit _hit)
    {
        RaycastHit hit = default(RaycastHit);
        if (Physics.Raycast(camera.transform.position, camera.forward, out hit, rayCastRange, hitLayer))
        {
            _hit = hit;
            return true;
        }
        _hit = hit;
        return false;
    }
}
