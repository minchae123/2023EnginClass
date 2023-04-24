using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WooblyText : MonoBehaviour
{
    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        tmpText.ForceMeshUpdate();

        TMP_TextInfo textinfo = tmpText.textInfo;
        //Debug.Log(textinfo.characterCount);
        //Debug.Log(textinfo.characterInfo[0].character);

        for(int i =0; i < textinfo.characterCount; i++)
        {
            TMP_CharacterInfo cInfo = textinfo.characterInfo[i];

            if(cInfo.isVisible == false)
            {
                continue;
            }
            Vector3[] vertices = textinfo.meshInfo[cInfo.materialReferenceIndex].vertices;

            int vIdx0 = cInfo.vertexIndex;
            for (int j = 0; j < 4; j++)
            {
                Vector3 origin = vertices[vIdx0 + j];
                vertices[vIdx0 + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2f + origin.x), 0);
            }
 
        }
        /*var meshInfo = textinfo.meshInfo[0];
        meshInfo.mesh.vertices = meshInfo.vertices;
        tmpText.UpdateGeometry(meshInfo.mesh, 0);*/

        tmpText.UpdateVertexData();
    }

    private void Update()
    {
        tmpText.ForceMeshUpdate();

        TMP_TextInfo textinfo = tmpText.textInfo;

        for (int i = 0; i < textinfo.characterCount; i++)
        {
            TMP_CharacterInfo cInfo = textinfo.characterInfo[i];

            if (cInfo.isVisible == false)
            {
                continue;
            }
            Vector3[] vertices = textinfo.meshInfo[cInfo.materialReferenceIndex].vertices;

            int vIdx0 = cInfo.vertexIndex;
            for (int j = 0; j < 4; j++)
            {
                Vector3 origin = vertices[vIdx0 + j];
                vertices[vIdx0 + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2f + origin.x), 0);
            }

        }
        tmpText.UpdateVertexData();
    }
}
