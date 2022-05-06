using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StanleyParableUltraModDeluxe
{
    public class TheMod : MelonMod
    {

        public AchievementID achievementToUnlock = AchievementID.Tuesday;
        public AchievementID achievementToUnlock1 = AchievementID.SpeedRun;
        public AchievementID achievementToUnlock2 = AchievementID.SettingsWorldChampion;

        private Rect windoPosition = new Rect(20, 20, 500, 300);
        private string TextArea = "";

        public override void OnGUI()
        {
            windoPosition = GUI.Window(0, windoPosition, WindowFunction, "The Stanley Parable Ultra Mod Delux Menu");
        }

        private void WindowFunction(int id)
        {
            //x,y w,h

            TextArea = GUI.TextArea(new Rect(200, 65, 100, 30), TextArea);

            GUI.Label(new Rect(20, 30, 350, 50), "Current number of scene(s): " + SceneManager.sceneCountInBuildSettings.ToString());

            GUI.Label(new Rect(20, 120, 250, 20), "Keybinds: ");
            GUI.Label(new Rect(20, 140, 250, 20), "K - Get current scene info to Melon Consol");
            GUI.Label(new Rect(20, 160, 250, 20), "T - Unlock All Door in scene");
            GUI.Label(new Rect(20, 180, 250, 20), "L - Enable Jumping");
            GUI.Label(new Rect(20, 200, 250, 20), "P - Achievement Giver");
            GUI.Label(new Rect(20, 220, 250, 20), "O - Show hidden Achievemet menu");


            if (GUI.Button(new Rect(20, 70, 150, 20), "Load Selected Scene"))
            {
                try
                {
                    int temp = int.Parse(TextArea);
                    SceneManager.LoadScene(temp);
                }
                catch (Exception exc)
                {
                    //For when people screw up
                }
            }

            /* currently unused
            if (GUI.Button(new Rect(20, 120, 150, 20), "Level 2"))
            {

            }
            */

            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg("Scene was loaded. Name: " + sceneName + " int: " + buildIndex);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OpenAllDoors();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                AcheivementThing();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                JumpingThing();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                ShowAcheivementMenu();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                int sceneCount = SceneManager.sceneCountInBuildSettings;
                LoggerInstance.Msg("Scene Count: " + sceneCount);
                //LoggerInstance.Msg("Scene was loaded. Name: " + S + " int: " + buildIndex);
            }
        }

        public void AcheivementThing()
        {
            PlatformAchievements.UnlockAchievement(this.achievementToUnlock);
            PlatformAchievements.UnlockAchievement(this.achievementToUnlock1);
            PlatformAchievements.UnlockAchievement(this.achievementToUnlock2);
            LoggerInstance.Msg("Achievements Given");
        }

        public void JumpingThing()
        {
            foreach (BooleanConfigurable go in Resources.FindObjectsOfTypeAll(typeof(BooleanConfigurable)) as BooleanConfigurable[])
            {
                if (go.name == "CONFIGURABLE_STANLEYCANJUMP")
                {
                    go.SetValue(true);
                }
            }

            //StanleyController.Instance.UnfreezeMotionAndView();
            LoggerInstance.Msg("JumpingEnabled");
        }

        public void ShowAcheivementMenu()
        {
            foreach (BooleanConfigurable go in Resources.FindObjectsOfTypeAll(typeof(BooleanConfigurable)) as BooleanConfigurable[])
            {
                if (go.name == "CONFIGURABLE_ACHIEVEMENTS_UI_ENABLED_BOOL")
                {
                    go.SetValue(true);
                }
            }

            //StanleyController.Instance.UnfreezeMotionAndView();
            LoggerInstance.Msg("JumpingEnabled");
        }

        public void OpenAllDoors()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<RotatingDoor>() != null)
                {
                    RotatingDoor Doorscript = go.GetComponent<RotatingDoor>();
                    Doorscript.isLocked = false;
                }
            }
            LoggerInstance.Msg("Doors Unlocked");
        }
    }
}
