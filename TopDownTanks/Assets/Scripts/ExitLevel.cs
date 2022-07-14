using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameManager _gameManager;

    public LayerMask PlayerLayerMask;
    
    #endregion

    #region Functions
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if(((1 << col2D.gameObject.layer) & PlayerLayerMask) != 0)
        {
            _gameManager.SaveData();
            _gameManager.LoadNextLevel();
        }
    }
    #endregion
}
