using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Interaction
{
    public FieldOfView fieldOfView;
    public bool inSight = false;

    [System.Serializable]
    public enum MonsterType 
    { 
        monster1,
        monster2,
        monster3,
    }
    public MonsterType monsterType = MonsterType.monster1;
    private void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }
    public override void Interact()
    {
       
        switch (inSight)
        {
            case false:
                Debug.Log("Success!");
                switch (monsterType) 
                {
                    case MonsterType.monster1:
                        PlayerMovement.Instance.skinType = 1;
                        break;

                    case MonsterType.monster2:
                        PlayerMovement.Instance.skinType = 2;

                        break;

                    case MonsterType.monster3:
                        PlayerMovement.Instance.skinType = 3;

                        break;
                }
                break;

            case true:
                Debug.Log("Fail");
                break;
        }

      
    }
	
}
