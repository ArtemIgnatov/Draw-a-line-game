using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private string _playerColor;
    [SerializeField] private Collider2D _finish;

    private Collider2D _collider;
    private bool _canDraw;
    private Line _currentLine;
    public const float RESOLUTION = 0.1f;
    public List<Vector2> Points = new List<Vector2>();

    void Start()
    {
        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.isTrigger = true;
        _cam = Camera.main;
        _canDraw = false;
    }

    // Update is called once per frame
    void Update()
    {
        Draw(Cheker());
    }

    private void Clear()
    {
        if (_currentLine != null)
        {
            Line[] lines = FindObjectsOfType<Line>();
            foreach (Line line in lines)
                if (line.CompareTag(_playerColor)) Destroy(line.gameObject);
            Points.Clear();
        }
    }

    public List<Vector2> GetPoints()
    {
        return Points;
    }

    private bool Cheker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Создаем луч на основе позиции мыши
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Создаем переменную для сохранения информации о столкновении луча с объектом
            RaycastHit2D hit;

            // Выполняем лучевой выстрел и сохраняем информацию о столкновении с объектом в переменной hit
            if (hit = Physics2D.Raycast(ray.origin, ray.direction))
            {
                // Проверяем, находится ли точка столкновения на коллайдере объекта
                _canDraw = hit.collider == _collider;
            }
        }
        return _canDraw;
    }

    private void Draw(bool canDraw)
    {
        if (canDraw)
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                Clear();
                _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity, this.gameObject.transform);
            }

            if (Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);

            if (Input.GetMouseButtonUp(0))
            {
                // Проверяем закончена ли линия на финише

                if (!_finish.OverlapPoint(mousePos)) Clear();
                _canDraw = false;
            }
        }
    }
}
