/*
* Mad Level Manager by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MadLevelManager;

public class BackToGroupsScript : MonoBehaviour {

    public void LoadGroups() {
        MadLevel.LoadLevelByName("Group Select Screen");
    }

}