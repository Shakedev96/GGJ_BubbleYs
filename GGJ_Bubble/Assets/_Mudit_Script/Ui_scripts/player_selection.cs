using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_selection : MonoBehaviour
{
    public UI_scene_changer_and_allthat scenechanging;

    public int level_NUM1;
    public int level_NUM2;
    public int level_NUM3;

    public void level1()
    {
        scenechanging.start_scene = level_NUM1;
        scenechanging.Start_Game();
    }
    public void level2()
    {
        scenechanging.start_scene = level_NUM2;
        scenechanging.Start_Game();
    }

    public void level3()
    {
        scenechanging.start_scene = level_NUM3;
        scenechanging.Start_Game();
    }
    public void Rondom_level()
    {
        int randon_level_select = Random.Range(0, 3);
        switch (randon_level_select)
        {
            case 0:
                level1();
                break;
            case 1:
                level2();
                break;
            case 2:
                level3();
                break;
        }
    }
}
