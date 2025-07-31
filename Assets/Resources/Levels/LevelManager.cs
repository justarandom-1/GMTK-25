using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject health;

    [SerializeField] string winScene;

    [SerializeField] string loseScene;
    private RectTransform healthbar;

    protected int state;
    protected AudioSource audioSource;
    protected string nextScene;
    protected int maxLevel;

    protected float dialogueVol;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        instance = this;

        if (health != null)
            healthbar = health.GetComponent<RectTransform>();

        audioSource = GetComponent<AudioSource>();

        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        dialogueVol = 1.0f / audioSource.volume;
    }

    public void updateHealth()
    {
        if (Player.instance != null && healthbar != null)
        {
            float x = 357.5f - Player.instance.getHealth() * (35 + 357.5f);
            healthbar.offsetMin = new Vector2(x, healthbar.offsetMin.y);
            healthbar.offsetMax = new Vector2(-1 * x, healthbar.offsetMax.y);

            healthbar.gameObject.GetComponent<Animator>().Play("flash");
        }
    }

    public void Win()
    {
        int levelNum = SceneManager.GetActiveScene().name[5] - '0';

        nextScene = winScene;
        PlayerPrefs.SetInt("maxLevel", Mathf.Max(maxLevel, levelNum + 1));
        EndLevel();
    }

    public void Lose()
    {
        nextScene = loseScene;
        PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().name[5] - '0');
        EndLevel();
    }

    public void EndLevel()
    {
        state = 2;

        GameObject.Find("Black").GetComponent<Animator>().Play("fadeIn");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        state2();

        if (state != 2)
            Win();
    }

    protected void state2()
    {
        if (state == 2)
        {
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime / dialogueVol, 0);

            if (audioSource.volume == 0)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void PlayAudio(AudioClip a, float vol = 1.0f)
    {
        audioSource.PlayOneShot(a, vol);
    }

    public static float Vec2Deg(Vector2 v)
    {
        v.Normalize();
        float a = Mathf.Asin(v.y) * Mathf.Rad2Deg;
        if (v.x < 0)
            a = 180 - a;
        return a;
    }
    
    public static Vector2 Deg2Vec(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

}
