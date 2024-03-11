using UnityEngine;

public class RotateAllHex : MonoBehaviour
{
    public Transform[] rotateTransforms;

    public void Awake()
    {
        RotateAllHexs();
    }

    public void RotateAllHexs()
    {
        foreach (Transform t in rotateTransforms)
        {
            switch (Random.Range(0, 6))
            {
                case 0:
                    t.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    t.rotation = Quaternion.Euler(0, 60, 0);
                    break;
                case 2:
                    t.rotation = Quaternion.Euler(0, 120, 0);
                    break;
                case 3:
                    t.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 4:
                    t.rotation = Quaternion.Euler(0, 240, 0);
                    break;
                case 5:
                    t.rotation = Quaternion.Euler(0, 300, 0);
                    break;
            }
        }
    }
}