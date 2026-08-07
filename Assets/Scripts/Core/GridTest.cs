using UnityEngine;
using EscapeTheLava.Data;
using EscapeTheLava.Managers;

namespace EscapeTheLava.Core
{
    /// <summary>
    /// Temporary script used to test the logical grid.
    /// Remove it later after the grid system is verified.
    /// </summary>
    public class GridTest : MonoBehaviour
    {
        private void Start()
        {
            GridManager gridManager = new GridManager();

            gridManager.Initialize();

            GridData grid = gridManager.Grid;

            Debug.Log(
                $"Grid Size: {grid.Columns} x {grid.Rows}");

            Debug.Log(
                $"Total Cells: {grid.CellCount}");

            Debug.Log(
                $"Diamonds: {grid.Count(TileType.Diamond)}");

            Debug.Log(
                $"Lava: {grid.Count(TileType.Lava)}");

            Debug.Log(
                $"Islands: {grid.Count(TileType.Island)}");
        }
    }
}