using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        int tileToUse = Random.Range(0, tiles.Length);
        GameObject tile = Instantiate(tiles[tileToUse], transform.position, Quaternion.identity);
        tile.transform.parent = this.transform;
        tile.name = "Tile " + this.gameObject.name;
    }
}
