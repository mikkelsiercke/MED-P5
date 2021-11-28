using System.Collections.Generic;
using UnityEngine;

public class BlockPath
{
    private DataLogging dataLogger;
    private Transform transform;
    private List<Vector3> positions;
    private List<Vector3> rotations;

    public BlockPath(DataLogging dataLogger, Transform transform)
    {
        this.dataLogger = dataLogger;
        this.transform = transform;
        positions = new List<Vector3>();
        rotations = new List<Vector3>();
    }

    public void SetNewPosition(int index)
    {
        if (dataLogger.completed[index])
        {
            transform.position = positions[index];
        }
    }

    public void SetNewRotation(int index)
    {
        if (dataLogger.completed[index])
        {
            transform.localRotation = Quaternion.Euler(rotations[index]);
        }
    }

    public void AddPosition(Vector3 position)
    {
        positions.Add(position);
    }

    public Vector3 GetPosition(int index)
    {
        return positions[index];
    }

    public void AddRotation(Vector3 rotation)
    {
        rotations.Add(rotation);
    }

    public Vector3 GetRotation(int index)
    {
        return rotations[index];
    }
}