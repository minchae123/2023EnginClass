using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeChar
{
    public bool IsComplete = false;

    private Vector3[] _originPos;
    private float _currentTime = 0;
    private float _delayTime = 0;
    private float _effectTime = 0;
    private int _startIndex;

    private Color _startColor;
    private Color _endColor;

    public TypeChar(int start, Vector3[] vertices, Color32[] colors, Color startColor, Color endColor, 
                        float delayTime, float effectTime = 0.5f, float offset = 15f)
    {
        _originPos = new Vector3[4];
        for(int i = 0; i < 4; ++i)
        {
            Vector3 point = vertices[start + i];  //�̰� �ϴ� ������ ����3�� �����ϱ� ����.
            _originPos[i] = point;

            //�ε巯�� ����Ʈ�� �ʿ��ϵ��� �ʹ� ����
            if(i == 0 || i == 3)
            {
                vertices[start + i] = point + new Vector3(offset, 0, 0); //�������� �����¸�ŭ �� �� ��
            }else
            {
                vertices[start + i] = point + new Vector3(offset + 0.25f, 0, 0); //�������� ������ + 0.25f����
            }

            colors[start + i].a = 0; //������ 0���� ����
        }
        _delayTime = delayTime;
        _effectTime = effectTime;
        _startIndex = start;
        _startColor = startColor;
        _endColor = endColor;
    }

    public void UpdateMesh(Vector3[] vertices, Color32[] colors)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _delayTime || IsComplete) return;
        //������Ÿ���� ���� �������ų� �Ϸ�� ����Ʈ��� �ƹ��͵� ���� �ʴ´�.

        float time = _currentTime - _delayTime;
        float percent = time / _effectTime; 

        for(int i = 0; i < 4; i++)
        {
            vertices[_startIndex + i] = Vector3.Lerp(vertices[_startIndex + i], _originPos[i], percent);
            colors[_startIndex + i] = Color.Lerp(_startColor, _endColor, percent);
        }

        if(percent >= 1)
        {
            IsComplete = true;
        }
    }
}

public class RoadRunnerText : MonoBehaviour
{
    [SerializeField]
    private float _typeTime = 0.1f;
    [SerializeField]
    private Color _startColor, _endColor;

    private bool _isTyping = false;

    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && _isTyping == false)
        {
            _isTyping = true;
            StartEffect("This is GGM!! ������ �� ��μ�!");
        }
    }

    public void StartEffect(string text)
    {
        _tmpText.SetText(text);
        _tmpText.color = _endColor;
        _tmpText.ForceMeshUpdate();

        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        List<TypeChar> charList = new List<TypeChar>();
        TMP_TextInfo textInfo = _tmpText.textInfo;
        Vector3[] vertices = textInfo.meshInfo[0].vertices;
        Color32[] colors = textInfo.meshInfo[0].colors32;

        for(int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (charInfo.isVisible == false) continue;
            charList.Add(
                new TypeChar(charInfo.vertexIndex, vertices, colors, 
                            _startColor, _endColor, 
                            i * _typeTime, _typeTime));
        }

        bool isAllComplete = false;
        
        while(isAllComplete == false)
        {
            yield return null;
            foreach(TypeChar t in charList)
            {
                t.UpdateMesh(vertices, colors);
                isAllComplete = t.IsComplete;
            }
            _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);
        }

        _isTyping = false;
    }
}
