using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameConstants", menuName = "Lab 4/GameConstants", order = 0)]
public class GameConstants : ScriptableObject {
    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
    // .. other reset values 

    // for Consume.cs
    public  int consumeTimeStep =  10;
    public  int consumeLargestScale =  4;
    
    // for Break.cs
    public  int breakTimeStep =  30;
    public  int breakDebrisTorque =  10;
    public  int breakDebrisForce =  10;
    
    // for SpawnDebris.cs
    public  int spawnNumberOfDebris =  10;
    
    // for Rotator.cs
    public  int rotatorRotateSpeed =  6;

    // for EnemyController.cs
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public float groundSurface = -3.8f;

    
    // for testing
    public  int testValue;

    // Mario basic starting values
    public int playerMaxSpeed = 5;
    public int playerMaxJumpSpeed = 30;
    public int playerDefaultForce = 150;

    // Ground distance for spawn manager
    public  float groundDistance =  -3.8f;
}

