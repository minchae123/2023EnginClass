using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeChar
{
    public bool isComplete = false;

    private Vector3[] originPos;
    private float _curTime = 0;
    private float _delayTIme = 0;
    private float _effectTime = 0;
    private int _startindex;

    private Color _startColor;
    private Color _endColor;

    public TypeChar(int start, Vector3[] vertices, Color32[] colors, Color startColor, Color endColor, float delayTIme =0.5f, float effectTime = 0.5f, float offset = 15f)
    {
        originPos = new Vector3[4];
        for(int i = 0; i < 4; i++)
        {
            Vector3 point = vertices[start + i];
            originPos[i] = point;

            if(i ==0|| i == 3)
            {
                vertices[start + i] = point + new Vector3(offset, 0, 0); // 우측으로 오프셋만큼 더 간곳
            }
            else
            {
                vertices[start + i] = point + new Vector3(offset + 0.25f, 0, 0); // 우측으로 오프셋만큼 더 +0.25
            }
            colors[start + i].a = 0;
            _effectTime = effectTime;
            _delayTIme = delayTIme;
            _startindex = start;
            _startColor = startColor;
            _endColor = endColor;
        }
    }

    public void UpdateMesh(Vector3[] vertices, Color32[] colors)
    {
        _curTime += Time.deltaTime;
        if (_curTime < _delayTIme || isComplete) return;

        float time = _curTime - _delayTIme;
        float percent = time / _effectTime;
        
        for(int i = 0; i < 4; i++)
        {
            vertices[_startindex + i] = Vector3.Lerp(vertices[_startindex + i], originPos[i], percent);
            colors[_startindex + i] = Color.Lerp(_startColor, _endColor, percent);
        }

        if(percent >= 1)
        {
            isComplete = true;
        }
    }
}

public class RoadRunnerText : MonoBehaviour
{
    [SerializeField] private float typeTime = 0.1f;
    [SerializeField] private Color startColor, endColor;

    private bool isTyping = false;
    private TMP_Text tmpTxt;

    private void Awake()
    {
        tmpTxt = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && isTyping == false)
        {
            isTyping = true;
            StartEffect("가나다라마바사");
        }
    }

    private void StartEffect(string t)
    {
        tmpTxt.SetText(t);
        tmpTxt.color = endColor;
        tmpTxt.ForceMeshUpdate();

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        List<TypeChar> charList = new List<TypeChar>();
        TMP_TextInfo textinfo = tmpTxt.textInfo;
        Vector3[] vertices = textinfo.meshInfo[0].vertices;
        Color32[] color = textinfo.meshInfo[0].colors32;

        for(int i = 0; i < textinfo.characterCount; i++)
        {
            TMP_CharacterInfo charinfo = textinfo.characterInfo[i];
            if (charinfo.isVisible == false) continue;
            charList.Add(new TypeChar(charinfo.vertexIndex, vertices, color, startColor, endColor, i * typeTime, typeTime));
        }

        bool isAllComplete = false;

        while(isAllComplete == false)
        {
            yield return null;
            foreach(TypeChar t in charList)
            {
                t.UpdateMesh(vertices, color);
                isAllComplete = t.isComplete;
            }
            tmpTxt.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);
        }
    }
}
