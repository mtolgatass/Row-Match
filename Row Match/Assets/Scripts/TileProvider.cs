using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProvider : MonoBehaviour
{
    // MARK: - Public Variables
    public GameObject[] tiles;
    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // MARK: - Public Functions
    public GameObject DeliverTile(string color, Vector2 position)
    {
        if (color == "y")
        {
            return DeliverYellowTile(position);
        } else if (color == "r")
        {
            return DeliverRedTile(position);
        } else if(color == "g")
        {
            return DeliverGreenTile(position);
        } else if(color == "b")
        {
            return DeliverBlueTile(position);
        } else
        {
            return DeliverBackgroundTile(position);
        }
    }

    // MARK: - Private Functions
    private GameObject DeliverBackgroundTile(Vector2 position)
    {
        GameObject backgroundTile = Instantiate(tilePrefab, position, Quaternion.identity);
        backgroundTile.name = "Background";
        return backgroundTile;
    }

    private GameObject DeliverYellowTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[0], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "y";
        tile.name = "Yellow Tile";
        return tile;
    }

    private GameObject DeliverRedTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[1], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "r";
        tile.name = "Red Tile";
        return tile;
    }

    private GameObject DeliverGreenTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[2], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "g";
        tile.name = "Green Tile";
        return tile;
    }

    private GameObject DeliverBlueTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[3], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "b";
        tile.name = "Blue Tile";
        return tile;
    }
}
