using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMesh;
    [SerializeField] List<Color> _textColors = new List<Color>();
    [SerializeField] float _changeTime = 1f;
    
    private float _timer;
    private int _index;

    public void Start()
    {
        _textMesh.enabled = false;

        if (_textColors.Count == 0)
            _textMesh.color = Color.gray;
        else
            _textMesh.color = _textColors[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (!_textMesh.enabled || _textColors.Count == 0)
            return;

        _timer += Time.deltaTime;
        _textMesh.color = Color.Lerp(_textColors[_index], _textColors[_index == _textColors.Count-1 ? 0 : _index + 1], (_timer / _changeTime));

        if(_timer >= _changeTime)
        {
            _timer = 0f;
            _index = _index == _textColors.Count-1 ? 0 : _index + 1;
        }
    }


    public void ActivateColors()
    {
        _textMesh.enabled = true;
    }

}
