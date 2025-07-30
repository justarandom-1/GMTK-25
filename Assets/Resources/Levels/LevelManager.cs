using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject health;
    [SerializeField] GameObject compass;

    [SerializeField] string winScene;

    [SerializeField] string loseScene;
    private RectTransform healthbar;
    private RectTransform compassPointer;

    private float compassAngle = 3600;
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

        if (compass != null)
            compassPointer = compass.GetComponent<RectTransform>();
            
        dialogueVol = 1.0f / audioSource.volume;
    }

    public void updateHealth()
    {
        if (Player.instance != null)
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

        if (Player.instance.getHealth() == 1)
            PlayerPrefs.SetInt("noHits", PlayerPrefs.GetInt("noHits", 0) | (1 << (levelNum - 1)));

        if (Player.instance.getHealth() >= 0.9f)
            PlayerPrefs.SetInt("mostHits", PlayerPrefs.GetInt("mostHits", 0) | (1 << (levelNum - 1)));
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


        var allEnemies = FindObjectsByType<Dog>(FindObjectsSortMode.None);

        if (allEnemies.Length == 0)
        {
            if (state != 2)
                Win();
        }
        else
        {
            Vector2 playerPos = Player.instance.getPosition();

            Vector2 dist = (Vector2)allEnemies[0].transform.position - playerPos;

            foreach (Dog d in allEnemies)
            {
                Vector2 newDist = (Vector2)d.transform.position - playerPos;
                if (newDist.magnitude < dist.magnitude)
                {
                    dist = newDist;
                }
            }

            // if (compassAngle == 3600)
            // {
            //     compassAngle = VectorToAngle(dist);
            //     return;
            // }

            float angleDist = VectorToAngle(dist) - compassAngle;

            float shift = 120 * Time.deltaTime;
            
            if (angleDist > 0){
                if(angleDist < 180)
                    shift = Mathf.Min(shift, angleDist);
                else
                    shift = Mathf.Max(shift * -1, angleDist - 360);
            }
            else{
                if(angleDist > -180)
                    shift = Mathf.Max(shift * -1, angleDist);
                else
                    shift = Mathf.Min(shift, angleDist + 360);
            }

            compassAngle += shift;                
            
            compassPointer.rotation = Quaternion.Euler(0.0f, 0.0f, compassAngle);

            compassAngle = compassAngle % 360;

        }
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

    public static float VectorToAngle(Vector2 v)
    {
        v.Normalize();
        float a = Mathf.Asin(v.y) * Mathf.Rad2Deg;
        if (v.x < 0)
            a = 180 - a;
        return a;
    }
}
