using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    public float _speed;
    public float _bottomLevel = -10;
    private float distanceFromTheStart = 0;

    [SerializeField] private GameObject _board;
    private float _numberOfBoard = 0f;
    private float _boardPositionY;
    private float _boardAngle = 0f;
    private float _boardMaxAngle = 30f;
    private float _boardMaxUpOfAngle = 0.275f;
    private float _theDistanceItCreates = 5f;

    [SerializeField] private Slider _slider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Bag _bag;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);

        _camera.transform.position = new Vector3(transform.position.x + 3, transform.position.y + 4, transform.position.z - 9);
    }

    void Update()
    {
        if (transform.position.y < _bottomLevel)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetMouseButtonDown(0) && _numberOfBoard >=5)
        {
            _boardPositionY = transform.position.y - 1;

            for (int i = 0; i < 5; i++)
            {
                Instantiate(_board, new Vector3(0, _boardPositionY, transform.position.z + i), Quaternion.identity);
                _numberOfBoard -= 1;
            }
        }

        if (Input.GetMouseButton(0) && transform.position.z >= distanceFromTheStart && _numberOfBoard >= 1)
        {
            _boardAngle = _boardMaxAngle * _slider.value;
            _boardPositionY += _boardMaxUpOfAngle * _slider.value;

            Instantiate(_board, new Vector3(0, _boardPositionY, transform.position.z + _theDistanceItCreates), Quaternion.Euler(-_boardAngle, 0, 0));
            
            _boardPositionY += _boardMaxUpOfAngle * _slider.value;
            _bag.RemoveBackpack();
            _numberOfBoard -= 1;
        }

        if (Input.GetMouseButton(0) && transform.position.z >= distanceFromTheStart)
            distanceFromTheStart = transform.position.z + 1;

        if (Input.GetMouseButtonUp(0))
            _slider.value = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(0);
        }

        Destroy(other.gameObject);
        _bag.AddBackpack();
        _numberOfBoard++;
    }
}