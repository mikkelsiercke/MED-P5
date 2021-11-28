using UnityEngine;

public class BlockPaths : MonoBehaviour
{
    //Set to GameObject SoundOrder script is on
    public DataLogging dataLogger;

    private BlockPath blockPath;

    [SerializeField] private Vector3[] positions;

    [SerializeField] private Vector3[] rotations;

    private void Start()
    {
        blockPath = new BlockPath(dataLogger, gameObject.transform);

        foreach (var pos in positions)
        {
            blockPath.AddPosition(pos);
        }

        foreach (var rot in rotations)
        {
            blockPath.AddRotation(rot);
        }
    }

    private void Update()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            blockPath.SetNewPosition(i);
            blockPath.SetNewRotation(i);
        }
    }
}