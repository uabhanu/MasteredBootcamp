using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class AI : MonoBehaviour
{
    // Variables to handle what we need to send through to our state.
    private NavMeshAgent _agent; // To store the NPC NavMeshAgent component.
    private Animator _anim; // To store the Animator component.
    private State _currentState;
    
    [SerializeField] private Transform player;  
    // To store the transform of the player. This will let the guard know where the player is, so it can face the player and know whether it should be shooting or chasing (depending on the distance).
    // Bhanu has turned the public variable into SerializeField private to do the same thing but now the variable is not exposed to other classes as not required

    private void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>(); // Grab agents NavMeshAgent.
        _anim = this.GetComponent<Animator>(); // Grab agents Animator component.
        _currentState = new Idle(this.gameObject, _agent, _anim, player); // Create our first state.
    }

    private void Update()
    {
        _currentState = _currentState.Process(); // Calls Process method to ensure correct state is set.
    }
}
