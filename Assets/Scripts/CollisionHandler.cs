using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem ExplosionParticles;


    AudioSource audioSource;

    List <MeshRenderer> meshRenderers = new List<MeshRenderer>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("<color=#FF006C> this is start platform </color>");
                break;
            case "Finish":
                Debug.Log("<color=#03F7FF>this is finish platform</color>");
                StartSucessSequence();
                break;
            case "Obstacle":
                Debug.Log("this is obstacle");
                StartFailSequence(crash);
                break;
            default:
                Debug.Log("this is something else");
                break;
        }
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
            Debug.Log("This is the last level, loading first level");
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartSucessSequence()
    {
        //TODO: add particle effect
        audioSource.PlayOneShot(success);
        DisableVelocity();
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

     public void StartFailSequence(AudioClip clip)
    {
        if (!ExplosionParticles.isPlaying)
        {
            ExplosionParticles.Play();
        }
        //TODO: add particle effect
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
        }

        GetComponent<PlayerController>().mainEngineParticles.gameObject.SetActive(false);
        GetComponent<PlayerController>().leftThrusterParticles.gameObject.SetActive(false);
        GetComponent<PlayerController>().rightThrusterParticles.gameObject.SetActive(false);

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        DisableVelocity();
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void DisableVelocity()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
