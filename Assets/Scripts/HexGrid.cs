using System;
using UnityEngine;

public class HexGrid : MonoBehaviour {

    public int width;
    public int height;
    
    public HexCell hexCellPrefab;

    HexMesh hexMesh;
    private HexCell[] hexCells;

    /*
     * Pre-Start, initialize the array of cells, drawing each to this component's child mesh.
     */
    private void Awake()
    {
        this.hexMesh = GetComponentInChildren<HexMesh>();
        this.hexCells = new HexCell[this.height * this.width];
        for (int z = 0, i = 0; z < this.height; z++)
        {
            for (int x = 0; x < this.width; x++)
            {
                InitHexCell(x, z, i++);
            }
        }
    }

    /*
     * Initialize a single cell at a given offset from the origin (x, z) following these steps:
     *  - Instantiate a new HexCell
     *  - Set this grid as the HexCell's parent
     *  - Position the cell's center:
     *    - Shift each x position by the minimum radius of a cell (this are "pointy" hexes)
     *    - Shift each Z position by the maximum radius of a cell _plus_ half that shift again (1.5 * MAX_R)
     *    - In every odd row, shift each x position by an additional minimum radius of a cell
     */
    private void InitHexCell(int x, int z, int index)
    {
        HexCell cell = this.hexCells[index] = Instantiate<HexCell>(this.hexCellPrefab);
        cell.transform.SetParent(this.transform, false);
        cell.coordinates = HexCoordinates.FromOffset(x, z);
        cell.transform.localPosition = new Vector3(
            (x + (z % 2 * 0.5f)) * (HexUtils.MIN_R * 2f),
            0f,
            z * (HexUtils.MAX_R * 1.5f)
        );
    }

    private void Start()
    {
        this.hexMesh.AddHexes(this.hexCells);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnLeftClick();
        }
    }

    private void OnLeftClick()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            SelectCell(hit.point);
        }
    }

    private void SelectCell(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);
        HexCoordinates coordinates = HexCoordinates.FromWorldPosition(point);
        Debug.Log(coordinates.ToString());
    }
}
