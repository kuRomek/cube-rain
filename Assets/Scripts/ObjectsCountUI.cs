using TMPro;
using UnityEngine;

public class ObjectsCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalCubes;
    [SerializeField] private TextMeshProUGUI _totalBombs;
    [SerializeField] private TextMeshProUGUI _activeCubes;
    [SerializeField] private TextMeshProUGUI _activeBombs;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.OnTotalCountChanged += UpdateTotalCount;
        _bombSpawner.OnTotalCountChanged += UpdateTotalCount;
        _cubeSpawner.OnActiveCountChanged += UpdateActiveCount;
        _bombSpawner.OnActiveCountChanged += UpdateActiveCount;
    }

    private void OnDisable()
    {
        _cubeSpawner.OnTotalCountChanged -= UpdateTotalCount;
        _bombSpawner.OnTotalCountChanged -= UpdateTotalCount;
        _cubeSpawner.OnActiveCountChanged -= UpdateActiveCount;
        _bombSpawner.OnActiveCountChanged -= UpdateActiveCount;
    }

    private void UpdateTotalCount()
    {
        _totalCubes.text = $"Total cubes: {_cubeSpawner.CountAll}";
        _totalBombs.text = $"Total bombs: {_bombSpawner.CountAll}";
    }

    private void UpdateActiveCount()
    {
        _activeCubes.text = $"Active cubes: {_cubeSpawner.CountActive}";
        _activeBombs.text = $"Active bombs: {_bombSpawner.CountActive}";
    }
}
