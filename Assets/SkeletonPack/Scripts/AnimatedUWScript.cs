using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedUWScript : MonoBehaviour {

    public int uvAnimationTileX = 4; //Here you can place the number of columns of your sheet.//The above sheet has 24
    public int uvAnimationTileY = 4; //Here you can place the number of rows of your sheet.//The above sheet has 1
    public float framesPerSecond = 16f;

    public Material mat;

    private Vector2 offset;
//private var size : Vector2;
    private float index;



    void Awake() {
        index = 0;
    }


    void Update() {


        // Calculate index
        index = Time.time * framesPerSecond;
        // repeat when exhausting all frames
        index = index % (uvAnimationTileX * uvAnimationTileY);

        // Size of every tile
        //var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);

        // split into horizontal and vertical index
        var uIndex = index % uvAnimationTileX;
        var vIndex = index / uvAnimationTileX;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        offset = new  Vector2(uIndex * 0.25f, 1.0f - 0.25f - vIndex * 0.25f);

        mat.SetTextureOffset(Shader.PropertyToID("_Texture2"), offset);//renderer.material.SetTextureOffset ("_MainTex", offset);
        //renderer.material.SetTextureScale ("_MainTex", size);
    }
}
