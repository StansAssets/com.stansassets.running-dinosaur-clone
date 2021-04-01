using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] int dayTimeLength, nightTimeLength;
    
	bool m_IsDay;
    float m_TimeOfDayLength, m_TimeOfDayRemaining;
    MoonPhases m_MoonPhases;

    public void ScoreGained (float score)
    {
        m_TimeOfDayRemaining -= score;
        if (m_TimeOfDayRemaining <= 0) {
            BeginTimeOfDay (!m_IsDay);
        }

        m_MoonPhases.Visible = !m_IsDay;
        if (!m_IsDay)
            m_MoonPhases.UpdateNightTimeProgress (1f - (m_TimeOfDayLength - m_TimeOfDayRemaining) / m_TimeOfDayLength);
    }

    public void Reset ()
    {
        m_TimeOfDayRemaining = 0;
		BeginTimeOfDay (true);
        m_MoonPhases.Reset ();
    }
    
	void Start ()
    {
        m_MoonPhases = FindObjectOfType<MoonPhases> ();
    }

    void BeginTimeOfDay (bool isDay)
    {
        // TODO  switch time of day
        m_IsDay = isDay;
        m_TimeOfDayLength = isDay ? dayTimeLength : nightTimeLength;
        m_TimeOfDayRemaining += m_TimeOfDayLength;
    }

    /*void SetColors ()
    {
        m_ColorCurves.redChannel.MoveKey(0, new Keyframe(0, currentPhase));
        m_ColorCurves.redChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
        m_ColorCurves.greenChannel.MoveKey(0, new Keyframe(0, currentPhase));
        m_ColorCurves.greenChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
        m_ColorCurves.blueChannel.MoveKey(0, new Keyframe(0, currentPhase));
        m_ColorCurves.blueChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
    }*/
}
