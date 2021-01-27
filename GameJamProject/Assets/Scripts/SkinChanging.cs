using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanging : Interaction
{
    public override void Interact()
    {
        switch (PlayerMovement.Instance.skinType) 
        {
            case 0:

                break;

            case 1:
                PlayerMovement.Instance.activeSkin = PlayerMovement.Instance.skinType;
                PlayerMovement.Instance.skinType = 0;
                PlayerMovement.Instance.skinTime = 30.0f;
                break;

            case 2:
                PlayerMovement.Instance.activeSkin = PlayerMovement.Instance.skinType;
                PlayerMovement.Instance.skinType = 0;
                PlayerMovement.Instance.skinTime = 30.0f;

                break;

            case 3:
                PlayerMovement.Instance.activeSkin = PlayerMovement.Instance.skinType;
                PlayerMovement.Instance.skinType = 0;
                PlayerMovement.Instance.skinTime = 30.0f;

                break;
        }

    }


}
