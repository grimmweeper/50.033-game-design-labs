using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "ScriptableObjects/Powerup", order = 5)]
public class Powerup : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
	// index in the UI
    public PowerupIndex index;
	// texture in the UI
    public Texture powerupTexture;
    
    // list of things any powerup can do
    public int aboluteSpeedBooster;
    public int absoluteJumpBooster;

	// effect of powerup
    public int duration;

    public List<int> Utilise(){
        return new List<int> {aboluteSpeedBooster, absoluteJumpBooster};
    }

    public void Reset(){
        aboluteSpeedBooster = 0;
        absoluteJumpBooster = 0;
    }

    public void Enhance(int speedBooster, int jumpBooster){
        aboluteSpeedBooster += speedBooster;
        absoluteJumpBooster += jumpBooster;
    }
}