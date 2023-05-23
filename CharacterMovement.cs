using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float _moveSpeed; // —корость прохождени€ пути
    private float _time = 3f; // ¬рем€ за которое персонаж проходит путь
    private float radiusThreshold = 0.5f; // ƒопустимый радиус на линии, чтобы персонаж мог переместитьс€
    private List<Vector2> _points = new List<Vector2>(); //—писок всех координат линии
    private int _currentPointIndex = 0;
    private DrawManager _drawManager;
    public bool IsMoving { get; set; } //‘лаг движени€
    public bool IsLineFinished { get; set; } //‘лаг того, что лини€ нарисована

    void Start()
    {
        _drawManager = GetComponentInChildren<DrawManager>();
        IsMoving = false;
        IsLineFinished = false;
    }
    

    void Update()
    {
        _points = _drawManager.GetPoints(); // ѕолучаем список точек из DrawManager
       
        IsLineFinished = _points.Count > 0; // ѕровер€ем закончена лини€ движени€ или нет

        if (IsLineFinished) Move(IsMoving);
    }

    private void Move(bool isMoving)
    {
        if (isMoving)
        {
            _points = _drawManager.GetPoints(); // ѕолучаем список точек из DrawManager
            _moveSpeed = _points.Count / _time; // ¬ычисл€ем скорость с которой будет двигатьс€ персонаж

            if (Vector2.Distance(transform.position, _points[_currentPointIndex]) < radiusThreshold)
            {
                // ≈сли позици€ персонажа и текущей точки наход€тс€ в допустимом радиусе, перемещаем персонаж к следующей точке
                if (_currentPointIndex < _points.Count - 1)
                {
                    _currentPointIndex++;
                }
                else
                {
                    // ≈сли это последн€€ точка, останавливаем персонаж
                    IsMoving = false;
                    _points.Clear();
                    _currentPointIndex = 0;
                    return;
                }
            }
            // ѕеремещаем персонажа к текущей точке
            transform.position = Vector2.MoveTowards(transform.position, _points[_currentPointIndex], _moveSpeed * Time.deltaTime);
        }
    }
}
