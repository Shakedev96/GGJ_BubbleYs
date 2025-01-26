using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class last_man_win : MonoBehaviour
{
    [SerializeField] private GameObject[] player_obj;
    [SerializeField] private int survival;
    [SerializeField] private GameObject win_canvas;

    private void Awake()
    {
        win_canvas.SetActive(false);
    }

    private void Update()
    {
        player_obj = GameObject.FindGameObjectsWithTag("Player");

        survival = player_obj.Length;

        if(survival == 1)
        {
            win_canvas.SetActive(true);
        }

    }

}
