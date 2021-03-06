using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TileProvider : MonoBehaviour
{
    // MARK: - Public Variables
    public GameObject[] tiles;

    // MARK: - Public Functions
    public GameObject DeliverTile(string color, Vector2 position)
    {
        if (color == "y")
        {
            return DeliverYellowTile(position);
        }
        else if (color == "r")
        {
            return DeliverRedTile(position);
        }
        else if (color == "g")
        {
            return DeliverGreenTile(position);
        }
        else if (color == "b")
        {
            return DeliverBlueTile(position);
        }
        else
        {
            return DeliverCompleteTile(position);
        }
    }

    // MARK: - Private Functions

    private GameObject DeliverYellowTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[0], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "y";
        tile.name = "Yellow Tile At Column: " + position.x + " Row; " + position.y;
        return tile;
    }

    private GameObject DeliverRedTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[1], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "r";
        tile.name = "Red Tile At Column: " + position.x + " Row; " + position.y;
        return tile;
    }

    private GameObject DeliverGreenTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[2], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "g";
        tile.name = "Green Tile At Column: " + position.x + " Row; " + position.y;
        return tile;
    }

    private GameObject DeliverBlueTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[3], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "b";
        tile.name = "Blue Tile At Column: " + position.x + " Row; " + position.y;
        return tile;
    }

    private GameObject DeliverCompleteTile(Vector2 position)
    {
        GameObject tile = Instantiate(tiles[4], position, Quaternion.identity);
        tile.GetComponent<Tile>().color = "c";
        tile.GetComponent<Tile>().canSwipe = false;
        tile.name = "Complete Tile At Column: " + position.x + " Row; " + position.y;
        return tile;
    }
}
