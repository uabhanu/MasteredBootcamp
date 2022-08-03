using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    public GameObject ObjectToInstantiate;

    public void InstantiateObject()
    {
        Instantiate(ObjectToInstantiate);
    }
}
