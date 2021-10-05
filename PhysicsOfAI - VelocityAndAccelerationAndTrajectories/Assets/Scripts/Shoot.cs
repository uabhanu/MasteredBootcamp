using UnityEngine;

public class Shoot : MonoBehaviour
{
    private const float Speed = 15;
    private const float TurnSpeed = 2;
    private bool _canShoot = true;
    
    public GameObject shellPrefab; // Add Shell game object in the Inspector.
    public GameObject shellSpawnPos; // Add Cube game object in the Inspector.
    public GameObject target; // Add Enemy game object in the Inspector.
    public GameObject parent; // Add Tank game object in the Inspector.

    private void CanShootAgain()
    {
        _canShoot = true;
    }

    private void Fire()
    {
        if(_canShoot)
        {
            var shell = Instantiate(shellPrefab , shellSpawnPos.transform.position , shellSpawnPos.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = Speed * transform.forward; // Use 'forward' because it's the Z axis you want to shoot along.
            _canShoot = false;
            Invoke("CanShootAgain" , 0.2f); // Increase value to slow down rate of fire, decrease value to speed up rate of fire.
        }
    }

    private void Update()
    {
        var direction = (target.transform.position - parent.transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x , 0 , direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation , lookRotation , Time.deltaTime * TurnSpeed);

        var angle = RotateTurret();

        if(angle != null && Vector3.Angle(direction, parent.transform.forward) < 10) // When the angle is less than 10 degrees...
            Fire(); // ...start firing.
    }

    //Bhanu has never seen this and it's very nice to know that this will return float or null
    private float? RotateTurret()
    {
        float? angle = CalculateAngle(false); // Set to false for upper range of angles, true for lower range.

        if (angle != null) // If we did actually get an angle...
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f); // ... rotate the turret using EulerAngles because they allow you to set angles around x, y & z.
        }
        
        return angle;
    }

    private float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = Speed * Speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if(underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);

        }
        else
            return null;
    }
}
