using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{

    #region Fields

    [SerializeField] private int _width = 15;
    [SerializeField] private int _height = 10;
    [SerializeField] private int _cameraDistance = 10;


    [SerializeField] private Transform _camera = null;

    [SerializeField] private Tile _tilePrefab = null;

    private Dictionary<Vector2, Tile> _tiles = null;
    #endregion Fields

    #region Properties
    #endregion Properties

    #region Methods
    private void Start()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        GenerateGrid();
    }
    void GenerateGrid()
    {
        int x = 0;

        for(int z = 0; z < _width; z++)
        {
            for(int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, z), Quaternion.identity);
                spawnedTile.name = "Tile" + z + y;


                //CHECK IF ITS A OFFSET TILE OR NOT
                bool isOffset = false;
                if ((z % 2 == 0 && y % 2 != 0) || (z % 2 != 0 && y % 2 == 0))
                {
                    isOffset = true;
                }

                //START THE INIT
                spawnedTile.Init(isOffset, z, y);

                _tiles[new Vector2(z, y)] = spawnedTile;
            }
        }

        _camera.transform.position = new Vector3(_cameraDistance, (float)_height / 2 - 0.5f, (float)_width / 2 - 0.5f); // THE _cameraDistance WILL HAVE TO BE ADJUSTED TO FIT THE FUCKING GRID

    }

    public Tile GetTileWithVector(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }

        return null;
    }
    #endregion Methods
 
}
