using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DEMO : MonoBehaviour {

    public GameObject player;
    public Camera _camera;
    public _map map;
	// Use this for initialization
	void Start () {
        _character character = player.GetComponent<_character>();
        character.set_current_cell(map.get_start_cell(player));
        show_movement_possibilities();
    }
	
	// Update is called once per frame
	void Update () {
        _character chara = player.GetComponent<_character>();
        if (chara.return_path_ended() == true)
        {
            _character character = player.GetComponent<_character>();
            if (character.return_walkable_cells() != null)
                map.clear_cells(character.return_walkable_cells());
            show_movement_possibilities();
            chara.set_path_ended(false);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject hit = map.case_touchee(_camera);
            hit.GetComponent<_cell>().change_color(Color.red);
            hit.GetComponent<_cell>().set_up_is_obstacle(true);
        }
        waiting_click_to_move();
	}
    public void show_movement_possibilities()
    {
        _character character = player.GetComponent<_character>();
        character.set_walkable_cells(map.get_walkable_cells(character.return_current_cell(), character.max_movement));
        map.change_cells_colors(character.return_walkable_cells(), Color.blue);

    }
    void waiting_click_to_move()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.GetComponent<_character>().return_is_walking() == false)
        {
            _character character = player.GetComponent<_character>();
            GameObject hit = map.case_touchee(_camera);
            bool is_hit_ok = map.check_hit_cell(character.return_walkable_cells(), hit);
            if (is_hit_ok == true)
            {
                character.set_path(map.get_path(character.return_current_cell(), hit));
                character.set_move(true);
            }
            if (is_hit_ok == false)
            {
                character.set_move(false);
            }

        }
    }
}
