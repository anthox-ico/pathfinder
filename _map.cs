using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _map : MonoBehaviour {

    public GameObject prefab_case;
    _pathfinder pathfinder = new _pathfinder();
    public bool is_generate_map;
    public float cell_size = 1;
    public int Tall_X = 25;
    public int Tall_Z = 25;

    List<GameObject> all_cases = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        if (is_generate_map == false)
        {
            int count = 0;
            while (count < gameObject.transform.childCount)
            {
                all_cases.Add(gameObject.transform.GetChild(count).gameObject);
                count++;
            }
        }
        if (is_generate_map == true)
        {
            generate_map();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /* map generation */
    public void generate_map()
    {

        int x = 0;
        int z = 0;
        while (z < Tall_Z)
        {
            while (x < Tall_X)
            {
                GameObject new_case = Instantiate(prefab_case) as GameObject;
                new_case.transform.localScale = new Vector3(cell_size, cell_size, cell_size);
                new_case.transform.position = new Vector3(x * cell_size, 0, z * cell_size);
                new_case.transform.parent = gameObject.transform;
                all_cases.Add(new_case);
                x++;
            }
            x = 0;
            z++;
        }
        assign_voisins();
    }
    public void assign_voisins()
    {
        foreach (GameObject cell in all_cases)
        {
            foreach (GameObject voisin in all_cases)
            {
                if (cell.transform.position.z == voisin.transform.position.z)
                {
                    if (cell.transform.position.x + cell_size == voisin.transform.position.x || cell.transform.position.x - cell_size == voisin.transform.position.x)
                    {
                        cell.GetComponent<_cell>().voisins.Add(voisin);
                    }
                }
                if (cell.transform.position.x == voisin.transform.position.x)
                {
                    if (cell.transform.position.z + cell_size == voisin.transform.position.z || cell.transform.position.z - cell_size == voisin.transform.position.z)
                    {
                        cell.GetComponent<_cell>().voisins.Add(voisin);
                    }
                }
            }
        }
    }
    public List<GameObject> return_all_cases()
    {
        return all_cases;
    }

    /* set up player */
    public GameObject get_start_cell(GameObject character)
    {
        GameObject cases = null;
        foreach (GameObject c in all_cases)
        {
            if (Vector3.Distance(c.transform.position, character.transform.position) <= cell_size / 5)
            {
                Debug.Log(" start cell trouvée ");
                cases = c;
            }
        }
        return cases;

    }
    public GameObject case_touchee(Camera camera)
    {
        GameObject final_target = null;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            _cell _C = objectHit.GetComponent<_cell>();
            foreach (GameObject c in return_all_cases())
            {
                if (objectHit == c)
                {
                    final_target = c;
                }
            }
        }
        return final_target;
    }
    public bool check_hit_cell(GameObject[] current_using_cells, GameObject hit)
    {
        foreach (GameObject g in current_using_cells)
        {
            if (hit == g)
                return true;
        }
        return false;
    }
    public GameObject[] get_path(GameObject start_target, GameObject final_target)
    {
        return pathfinder.get_path(start_target, final_target);
    }

    // movement
    public GameObject[] get_walkable_cells(GameObject start_target, int distance)
    {
        GameObject[] reachable_cells = pathfinder.walkable_range(start_target, distance);
        GameObject[] walkable_cells = find_walkable_cells(reachable_cells);
        return walkable_cells;
    }
    GameObject[] find_walkable_cells(GameObject[] to_check_in)
    {
        List<GameObject> walkables = new List<GameObject>();
        foreach (GameObject g in to_check_in)
        {
            if (g.GetComponent<_cell>().return_is_obstacle() == false)
            {
                walkables.Add(g);
            }
        }
        return walkables.ToArray();
    }

    /* map */
    public void clear_cells(GameObject[] cells)
    {
        foreach (GameObject c in cells)
        {
            c.GetComponent<_cell>().set_previous_case(null);
            c.GetComponent<_cell>().current_cost = 0;
        }
        change_cells_colors(cells, Color.white);
    }
    public void change_cells_colors(GameObject[] c, Color _c)
    {
        foreach (GameObject cell in c)
        {
            cell.GetComponent<_cell>().change_color(_c);
        }
    }
    public void reset_map()
    {
        clear_cells(all_cases.ToArray());
        foreach (GameObject c in all_cases)
        {
            c.GetComponent<_cell>().set_up_is_obstacle(false);
        }
    }
    public void destroy_map()
    {
        foreach (GameObject g in all_cases)
        {
            Destroy(g);
        }
        Destroy(pathfinder);
        all_cases.Clear();
        pathfinder = null;
        pathfinder = new _pathfinder();
    }
}
