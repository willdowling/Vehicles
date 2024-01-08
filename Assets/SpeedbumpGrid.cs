using UnityEngine;
using UnityEngine.UI;  // Add this line

public class SpeedbumpGrid : MonoBehaviour
{
    public GameObject SpeedbumpPrefap;
    public int rows = 3;
    public float spacing = 2f;

    void Start()
    {
        CreateSpeedbumpGrid();
    }

    void CreateSpeedbumpGrid()
    {
        GridLayoutGroup gridLayout = gameObject.AddComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(spacing, spacing);

        for (int row = 0; row < rows; row++)
        {
            Instantiate(SpeedbumpPrefap, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + row * spacing), new Quaternion(-0.5f,0.5f,0.5f,0.5f), transform);
        }
    }
}
