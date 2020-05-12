using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector3 startPosition;      // Start position to reset the car
    private Vector3 startRotation;      // Start rotation to reset the car

    [Range(-1f, 1f)]
    public float acceleration, turn;    //Acceleration and turning value

    public float timeSinceStart = 0f;   // Check if the car has been idle for so much time and must be reset

    [Header("Fitness")]
    public float overallFitness;                // Fitness value for genetic algorithm
    public float distanceMultiplier = 1.4f;     // Distance importance for fitness function
    public float avgSpeedMultiplier = 0.2f;     // Speed importance for fitness function
    public float sensorMultiplier = 0.1f;       // Sensor importance for fitness function (stay in the middle of the street)

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private float aSensor, bSensor, cSensor;


    public void Update()
    {
        getHumanInput();
    }

    private void getHumanInput()
    {
        float h = Input.GetAxis("Horizontal");
        turn += 0.02f * h;

        float v = Input.GetAxis("Vertical");
        acceleration += 0.2f * v;

        if (turn > 1) turn = 1;
        if (turn < -1) turn = -1;
        if (acceleration > 1) acceleration = 1;
        if (acceleration < -1) acceleration = -1;
    }

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
    }

    public void Reset()
    {
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        acceleration = 0;
        turn = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Reset();
    }

    private void FixedUpdate()
    {
        InputSensors();
        lastPosition = transform.position;

        // Neural network code here
        MoveCar(acceleration, turn);

        timeSinceStart += Time.deltaTime;

        CalculateFitness();

        //a=0
        //t=0
    }


    private void CalculateFitness()
    {
        // Distance contribution
        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        float distanceContribution = distanceMultiplier * totalDistanceTravelled;

        // Speed contribution
        avgSpeed = totalDistanceTravelled / timeSinceStart;
        float speedContribution = avgSpeedMultiplier * avgSpeed;

        // Sensor contribution
        float sensorContribution = sensorMultiplier * (aSensor + bSensor + cSensor) / 3;

        overallFitness = distanceContribution + speedContribution + sensorContribution;

        if (timeSinceStart > 20 && overallFitness < 40)
        {
            Reset();
        }

        if (overallFitness >= 1000)
        {
            // Saves the network to a JSON
            Reset();
        }
    }


    private void InputSensors()
    {
        Vector3 a = (transform.forward + transform.right); // Right 45º angle sensor.
        Vector3 b = transform.forward;                     // Forward sensor.
        Vector3 c = (transform.forward - transform.right); // Left 45º angle sensor.

        Ray r = new Ray(transform.position, a);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit))
        {
            aSensor = hit.distance / 20;
            //print("A sensor: " + aSensor);
        }

        r.direction = b;
        if (Physics.Raycast(r, out hit))
        {
            bSensor = hit.distance / 20;
            //print("B sensor: " + bSensor);
        }

        r.direction = c;
        if (Physics.Raycast(r, out hit))
        {
            cSensor = hit.distance / 20;
            //print("C sensor: " + cSensor);
        }
    }


    // v = vertical movement  h = horizontal movement
    private Vector3 inp;
    public void MoveCar(float v, float h)
    {
        inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), 0.02f); // Make a linear Interpolation between two vector
        inp = transform.TransformDirection(inp);    // Transform the inp vector in the local system of the car to ensure that
                                                    // the car go forward.
        transform.position += inp;

        transform.eulerAngles += new Vector3(0, h*90*0.02f, 0); // h is the number of times we turn left or right
    }


}
