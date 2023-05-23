using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float _moveSpeed; // �������� ����������� ����
    private float _time = 3f; // ����� �� ������� �������� �������� ����
    private float radiusThreshold = 0.5f; // ���������� ������ �� �����, ����� �������� ��� �������������
    private List<Vector2> _points = new List<Vector2>(); //������ ���� ��������� �����
    private int _currentPointIndex = 0;
    private DrawManager _drawManager;
    public bool IsMoving { get; set; } //���� ��������
    public bool IsLineFinished { get; set; } //���� ����, ��� ����� ����������

    void Start()
    {
        _drawManager = GetComponentInChildren<DrawManager>();
        IsMoving = false;
        IsLineFinished = false;
    }
    

    void Update()
    {
        _points = _drawManager.GetPoints(); // �������� ������ ����� �� DrawManager
       
        IsLineFinished = _points.Count > 0; // ��������� ��������� ����� �������� ��� ���

        if (IsLineFinished) Move(IsMoving);
    }

    private void Move(bool isMoving)
    {
        if (isMoving)
        {
            _points = _drawManager.GetPoints(); // �������� ������ ����� �� DrawManager
            _moveSpeed = _points.Count / _time; // ��������� �������� � ������� ����� ��������� ��������

            if (Vector2.Distance(transform.position, _points[_currentPointIndex]) < radiusThreshold)
            {
                // ���� ������� ��������� � ������� ����� ��������� � ���������� �������, ���������� �������� � ��������� �����
                if (_currentPointIndex < _points.Count - 1)
                {
                    _currentPointIndex++;
                }
                else
                {
                    // ���� ��� ��������� �����, ������������� ��������
                    IsMoving = false;
                    _points.Clear();
                    _currentPointIndex = 0;
                    return;
                }
            }
            // ���������� ��������� � ������� �����
            transform.position = Vector2.MoveTowards(transform.position, _points[_currentPointIndex], _moveSpeed * Time.deltaTime);
        }
    }
}
