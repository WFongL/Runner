using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private GameObject _board;
    [SerializeField] Transform _bag;
    private float _distanceOfBoards = 0.08f;

    private List<GameObject> _bagBoards = new List<GameObject>();

    public void AddBackpack()
    {
        GameObject curPrefab;
        if (_bagBoards.Count != 0)
        {
            curPrefab = Instantiate(_board, new Vector3(0f, _bagBoards[_bagBoards.Count - 1].transform.position.y + _distanceOfBoards, _bag.transform.position.z),
                                    Quaternion.identity, _bag);
        }
        else
        {
            curPrefab = Instantiate(_board, _bag.transform.position, Quaternion.identity, _bag);
        }
        _bagBoards.Add(curPrefab);
    }

    public void RemoveBackpack()
    {
        if (_bagBoards.Count != 0)
        {
            Destroy(_bagBoards[_bagBoards.Count - 1].gameObject);
            _bagBoards.Remove(_bagBoards[_bagBoards.Count - 1].gameObject);
        }
    }
}
