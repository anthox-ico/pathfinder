using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _character : MonoBehaviour {

    public int max_movement = 6;
    public int speed = 2;

    bool is_walking = false;
    bool path_ended = false;
    GameObject current_cell = null;
    GameObject[] walkable_cells = null;   
    GameObject[] path = null;

    // Use this for initialization
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (is_walking == true)
        {
            move();
        }
    }

    /* cells */
    public void set_current_cell(GameObject c)
    {
        current_cell = c;
    }
    public GameObject return_current_cell()
    {
        return current_cell;
    }
    // movement
    public bool return_is_walking()
    {
        return is_walking;
    }
    public void set_walkable_cells(GameObject[] c)
    {
        walkable_cells = c;
    }
    public void set_path(GameObject[] p)
    {
        path = p;
    }
    public GameObject[] return_path()
    {
        return path;
    }
    public GameObject[] return_walkable_cells()
    {
        return walkable_cells;
    }
    public void set_move(bool b)
    {
        is_walking = b;
    }
    public void move()
    {
        int count = 0;
        while (path[count] == null && count < path.Length - 1)
        {
            count++;
        }
        if (Vector3.Distance(gameObject.transform.position, path[count].transform.position) >= 0.2f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, path[count].transform.position, gameObject.GetComponent<_character>().speed * Time.deltaTime);
        }
        if (Vector3.Distance(gameObject.transform.position, path[count].transform.position) <= 0.2f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, path[count].transform.position, gameObject.GetComponent<_character>().speed * Time.deltaTime);
            current_cell = path[count];
            path[count] = null;
            if (count == path.Length - 1)
            {
                Debug.Log("char arv case finale");
                path_ended = true;
                path = null;
                is_walking = false;
            }
        }
    }
    public bool return_path_ended()
    {
        return path_ended;
    }
    public void set_path_ended(bool b)
    {
        path_ended = b;
    }
}
