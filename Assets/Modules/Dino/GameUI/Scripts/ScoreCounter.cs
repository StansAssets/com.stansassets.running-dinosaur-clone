using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ScoreCounter : MonoBehaviour, IDinoInGameUI
{
    const string k_Format = "D5";

    // Text components displaying the current score. As far as the score is updated every frame, there is no need to make it more complicated.
    [SerializeField] Text m_ScoreText, m_HighScoreText;
    [SerializeField] RectTransform m_HealthbarRoot, m_LiveIcon;

    float m_Score;
    int m_HighScore;
    int m_Hundreds;
    AudioSource m_Source;

    int m_Lives;
    List<RectTransform> m_LivesImages = new List<RectTransform>();

    void Start ()
    {
        // it is expected to have an attached AudioSource with 'another 100 score' sound selected
        m_Source = GetComponent<AudioSource> ();
    }

    public float Score {
		get => m_Score;
		private set {
            m_Score = value;
            int roundedValue = Mathf.FloorToInt (m_Score);
            HighScore = Mathf.Max (HighScore, roundedValue);

            if (m_Score > m_Hundreds) {
                while (m_Score > m_Hundreds) { m_Hundreds += 100; }
                m_Source.Play ();
            }

            m_ScoreText.text = roundedValue.ToString (k_Format);
        }
	}

    public int HighScore {
        get => m_HighScore;
        set {
            m_HighScore = value;
            m_HighScoreText.text = "HI " + value.ToString (k_Format);
        }
    }

    public void AddPoints (float pts) => Score += pts;

    public void SetLivesAmount(int lives)
    {
        var existingImages = m_LivesImages.Count;
        m_Lives = lives;
        if (m_Lives > existingImages)
        {
            for (int i = 0; i < m_Lives - existingImages; i++)
            {
                var icon = Instantiate(m_LiveIcon, m_HealthbarRoot, true);
                icon.gameObject.SetActive(true);
                m_LivesImages.Add(icon);
            }
        }
        else
        {
            for (int i = 0; i < existingImages - m_Lives; i++)
            {
                var icon = m_LivesImages[m_LivesImages.Count - 1];
                m_LivesImages.Remove(icon);
                Destroy(icon.gameObject);
            }
        }
    }

    public void Reset ()
    {
        m_Hundreds = 100;
        Score = 0;
    }

    public void SetPause (bool paused) { }
}
