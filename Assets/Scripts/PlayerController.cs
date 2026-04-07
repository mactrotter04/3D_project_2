using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainEngine;

    [Header("particles")]
    public ParticleSystem mainEngineParticles;
    public ParticleSystem leftThrusterParticles;
    public ParticleSystem rightThrusterParticles;

    [Header("Fuel")]
    [SerializeField] float maxFuel = 100f;        
    [SerializeField] float thrustFuelBurn = 20f;
    [SerializeField] float rotationFuelBurn = 10f;
    [SerializeField] Slider fuelSlider;
    float currentFuel;

    Rigidbody rb;
    AudioSource audiosource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
        UpdateFuelUI();
        fuelSlider.maxValue = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRoatation();
        UpdateFuelUI();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && currentFuel > 0f)
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            currentFuel -= thrustFuelBurn * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else
        {
            audiosource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRoatation()
    {
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            ApplyRoation(rotationThrust);
            currentFuel -= rotationFuelBurn * Time.deltaTime;
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            ApplyRoation(-rotationThrust);
            currentFuel -= rotationFuelBurn * Time.deltaTime;
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    void ApplyRoation(float roatationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * roatationThisFrame);
        rb.freezeRotation = false;
    }

    void UpdateFuelUI()
    {
        fuelSlider.value = currentFuel;
    }

}
