using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _pathfinder : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    GameObject get_lowest_case(List<GameObject> to_check)
    {
        float lowest_cost = 0;
        float checking_cost = 0;
        GameObject lowest_cost_case = null;

        foreach (GameObject c in to_check) // regarde les cases a checker , en partant de la start target , et choisi la plus proche
        {
            checking_cost = c.GetComponent<_cell>().return_current_cost();
            if (lowest_cost == 0)
            {
                lowest_cost = checking_cost;
                lowest_cost_case = c;
            }
            if (lowest_cost >= checking_cost)
            {
                lowest_cost = checking_cost;
                lowest_cost_case = c;
            }
        }
        return lowest_cost_case;
    }
    public GameObject[] set_cells_cost(GameObject start_target, int max_distance)
    {
        List<GameObject> to_check = new List<GameObject>();
        List<GameObject> already_check = new List<GameObject>();
        List<GameObject> remove = new List<GameObject>();
        List<GameObject> for_add = new List<GameObject>();
        List<GameObject> reachable_cells = new List<GameObject>();
        int count = 0;
        to_check.Add(start_target);
        already_check.Add(start_target);
        start_target.GetComponent<_cell>().current_cost = 0;
        while (count < max_distance)
        {
            foreach (GameObject c in to_check)
            {
                foreach (GameObject v in c.GetComponent<_cell>().return_voisins())
                {
                    bool already_checked = false;
                    foreach (GameObject al in already_check)
                    {
                        if (v == al)
                        {
                            already_checked = true;
                        }
                    }
                    if (already_checked == false && v.GetComponent<_cell>().return_is_obstacle() == false)
                    {
                        v.GetComponent<_cell>().current_cost = 0;
                        already_check.Add(v);
                        var _V = v.GetComponent<_cell>();
                        _V.set_previous_case(c);
                        _V.add_current_cost(c.GetComponent<_cell>().return_current_cost());
                        reachable_cells.Add(v);
                        for_add.Add(v);
                    }
                }
                remove.Add(c);
            }
            foreach (GameObject r in for_add)
            {
                to_check.Add(r);
            }
            for_add.Clear();
            foreach (GameObject r in remove)
            {
                to_check.Remove(r);
                to_check.TrimExcess();
            }
            remove.Clear();
            count++;
        }
        return reachable_cells.ToArray();
    }
    public GameObject[] get_path(GameObject start_target, GameObject final_target)
    {
        List<GameObject> P = new List<GameObject>();
        GameObject current_case = final_target;

        while (current_case != start_target)
        {
            P.Add(current_case);
            current_case = current_case.GetComponent<_cell>().previous_case;
        }
        P.Reverse();
        return P.ToArray();
    } // get ordered path
}
