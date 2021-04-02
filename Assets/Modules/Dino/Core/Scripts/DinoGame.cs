using System;
using StansAssets.ProjectSample.Dino.Game;
using UnityEditor;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace StansAssets.ProjectSample.Dino
{
    public class DinoGame
    {
        public event Action OnHit;
        
        readonly Scene m_GamePlayScene;
        readonly IDinoInGameUI m_UI;


        public DinoGame (Scene targetScene, IDinoInGameUI ui)
        {
            m_UI = ui;
            m_GamePlayScene = targetScene;
            UnityEngine.Object.FindObjectOfType<DinoCharacter> ().OnHit += () => OnHit?.Invoke ();
            UnityEngine.Object.FindObjectOfType<DinoLevel> ().OnScoreGained += m_UI.AddPoints;
        }

        internal void Restart ()
        {
            
        }

        internal void Pause (bool isPaused)
        {
            
        }

        internal void Destroy ()
        {
            
        }

        internal void Start (Action callbackWhenCompleted)
        {
            m_UI.Reset ();
            callbackWhenCompleted?.Invoke ();
        }
    }
}

