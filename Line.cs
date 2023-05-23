using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _render;
    private DrawManager _drawManager;

    void Start()
    {
        _drawManager = GetComponentInParent<DrawManager>();
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        _render.positionCount++;
        _render.SetPosition(_render.positionCount -1, pos);
        if (_drawManager != null) _drawManager.Points.Add(pos);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (_render.positionCount == 0) return true;
        return Vector2.Distance(_render.GetPosition(_render.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }
}
