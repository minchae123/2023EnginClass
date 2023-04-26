using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWriterText : MonoBehaviour
{
    private TMP_Text _tmpText;
    private int _tIndex = 0;
    [SerializeField]
    private float _typeTime = 0.3f;
    [SerializeField]
    private Color _startColor, _endColor;
    private bool _isTyping = false;
    [SerializeField] string text = "Hello GGM! This is game!";
    [SerializeField] ParticleSystem particle;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _isTyping == false)
        {
            _tIndex = 0;
            _isTyping = true;
            StartEffect(text);
        }
        else if(Input.GetKeyDown(KeyCode.A) && _isTyping == true)
        {
            SkipEffect();
        }
    }

    public void SkipEffect()
    {
        StopAllCoroutines();
        TMP_TextInfo textinfo = _tmpText.textInfo;
        _tmpText.maxVisibleCharacters = textinfo.characterCount;
        _tmpText.ForceMeshUpdate();

        for(int i = _tIndex; i < textinfo.characterCount; i++)
        {
            StartCoroutine(TypeOneCharacter(textinfo, i));
        }
        _isTyping = false;

    }

    public void StartEffect(string text)
    {
        _tmpText.SetText(text);
        _tmpText.color = _endColor;
        _tmpText.ForceMeshUpdate();

        _tIndex = 0;
        _tmpText.maxVisibleCharacters = 0; //�ƹ� ���ڵ� �Ⱥ���

        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        TMP_TextInfo textInfo = _tmpText.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            yield return StartCoroutine(TypeOneCharacter(textInfo));
        }
        _isTyping = false;
    }


    private IEnumerator TypeOneCharacter(TMP_TextInfo textInfo, int idx = -1)
    {
        if (idx < 0)
        {
            _tmpText.maxVisibleCharacters = _tIndex + 1;
            _tmpText.ForceMeshUpdate();
        }

        TMP_CharacterInfo charInfo = textInfo.characterInfo[idx < 0 ? _tIndex : idx];

        if (charInfo.isVisible == false)
        {
            yield return new WaitForSeconds(_typeTime);
        }
        else
        {
            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            Color32[] vertexColors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

            int vIndex0 = charInfo.vertexIndex;
            int vIndex1 = vIndex0 + 1;
            int vIndex2 = vIndex0 + 2;
            int vIndex3 = vIndex0 + 3;

            Vector3 v1Origin = vertices[vIndex1];
            Vector3 v2Origin = vertices[vIndex2];

            float currentTime = 0;
            float percent = 0;

            while (percent < 1)
            {
                currentTime += Time.deltaTime;
                percent = currentTime / _typeTime;
                float yDelta = Mathf.Lerp(2f, 0, percent);

                vertices[vIndex1] = v1Origin + new Vector3(0, yDelta, 0);
                vertices[vIndex2] = v2Origin + new Vector3(0, yDelta, 0);

                for(int i = 0; i < 4; i++)
                {
                    vertexColors[vIndex0 + i] = Color.Lerp(_startColor, _endColor, percent);
                }
                
                _tmpText.UpdateVertexData();
                yield return null;
            }
            vertices[vIndex1] = v1Origin;
            vertices[vIndex2] = v2Origin;

            _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);
            Vector3 endPos = vertices[vIndex3];
            Vector3 worldEndPos = transform.TransformPoint(endPos);
            ParticleSystem p = Instantiate(particle, worldEndPos, Quaternion.Euler(-90,0,0))
                ;
            p.Play();
            Destroy(p.gameObject, 2f);
        }
        _tIndex++;
    }
}
