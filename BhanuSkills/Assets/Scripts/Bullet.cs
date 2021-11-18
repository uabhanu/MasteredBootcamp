using Bhanu;
using ScriptableObjects;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private Rigidbody bulletBody;

    private void Start()
    {
        bulletBody.velocity = transform.forward * bulletData.MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Target"))
        {
            LogMessages.AllIsWellMessage("Target Hit :)" % Colourize.Green % FontFormat.Bold);
        }
        else
        {
            LogMessages.AllIsWellMessage("Not the Target :(" % Colourize.Red % FontFormat.Bold);
        }
        
        Destroy(gameObject);
    }
}
