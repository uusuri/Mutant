using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BaseController : IDisposable
{
    private readonly List<BaseController> _baseControllers = new List<BaseController>();
    private readonly List<GameObject> _gameObjects = new List<GameObject>();

    private bool _isDisposed;

    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        foreach (var baseController in _baseControllers)
            baseController?.Dispose();
        _baseControllers.Clear();

        foreach (var cashedGameObject in _gameObjects)
            Object.Destroy(cashedGameObject);
        _gameObjects.Clear();

        OnDispose();
    }

    protected void AddController(BaseController controller)
    {
        _baseControllers.Add(controller);
    }

    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected virtual void OnDispose()
    {
    }
}