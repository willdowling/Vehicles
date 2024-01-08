using UnityEngine;
using UnityEngine.UI;  // Add this line

public class BumpySphereGrid : MonoBehaviour
{
    public GameObject bumpySpherePrefab;
    public int rows = 3;
    public int columns = 3;
    public float spacing = 2f;

    void Start()
    {
        CreateBumpySphereGrid();
    }

    void CreateBumpySphereGrid()
    {
        GridLayoutGroup gridLayout = gameObject.AddComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(spacing, spacing);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                print(new Vector3(col * spacing, row * spacing, 0));
                Instantiate(bumpySpherePrefab, new Vector3(this.gameObject.transform.position.x + col * spacing, this.gameObject.transform.position.y, this.gameObject.transform.position.z + row * spacing), Quaternion.identity, transform);
            }
        }
    }
}
