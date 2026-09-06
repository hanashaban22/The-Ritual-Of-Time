using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] walls;
    private int currentWallIndex = 0;

    void Start()
    {
        UpdateWalls();
    }

    public void MoveRight()
    {
        currentWallIndex++;
        if (currentWallIndex >= walls.Length) currentWallIndex = 0;
        UpdateWalls();
    }

    public void MoveLeft()
    {
        currentWallIndex--;
        if (currentWallIndex < 0) currentWallIndex = walls.Length - 1;
        UpdateWalls();
    }

    private void UpdateWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].SetActive(i == currentWallIndex);
        }
    }
}