using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class YAxisSpriteSorting : MonoBehaviour
{

    Renderer spriteRendrer;
    public bool IsGameObjectStatic;

    void Start()
    {
        spriteRendrer = GetComponent<Renderer>();
        spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
    }


    // 
//#if UNITY_EDITOR // This code will only run when in editor
    void Update()
    {
        if(!IsGameObjectStatic)
        {
            spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
        }
    }

//#endif

}