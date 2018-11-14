using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _cell : MonoBehaviour {

    public bool is_obstacle = false;
    public float current_cost = 0;
    public float case_cost = 1;
    public List<GameObject> voisins = new List<GameObject>();
    public GameObject previous_case = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /* graphics */
    public void change_color(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }
    public void clear_case(Color c)
    {
        change_color(c);
        current_cost = 0;
    }
    public void add_voisin(GameObject v)
    {
        voisins.Add(v);
    }
    public void add_current_cost(float cost_to_add)
    {
        current_cost += cost_to_add + case_cost;
    }
    public void add_cost(float cost_to_add)
    {
        current_cost += cost_to_add;
    }
    public void set_previous_case(GameObject c)
    {
        previous_case = c;
    }

    public bool return_is_obstacle()
    {
        return is_obstacle;
    }
    public GameObject[] return_voisins()
    {
        return voisins.ToArray();
    }
    public GameObject return_previous_case()
    {
        return previous_case;
    }
    public float return_current_cost()
    {
        return current_cost;
    }
    public void set_up_is_obstacle(bool b)
    {
        is_obstacle = b;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            is_obstacle = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            is_obstacle = false;
        }
    }
    */
}
