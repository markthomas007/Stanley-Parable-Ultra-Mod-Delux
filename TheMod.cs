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
using System.Reflection;

namespace StanleyParableUltraModDeluxe
{
    public class TheMod : MelonMod
    {

        bool menuShown = true;

        public AchievementID achievementToUnlock = AchievementID.Tuesday;
        public AchievementID achievementToUnlock1 = AchievementID.SpeedRun;
        public AchievementID achievementToUnlock2 = AchievementID.SettingsWorldChampion;

        private Rect windoPosition = new Rect(20, 20, 500, 420);
        private string TextArea = "";

        private float hSliderValue = 0.0f;

        public override void OnGUI()
        {
            if (menuShown == true)
            {
                windoPosition = GUI.Window(0, windoPosition, WindowFunction, "The Stanley Parable Ultra Mod Delux Menu");
            }
        }

        private void WindowFunction(int id)
        {
            //x,y w,h

            TextArea = GUI.TextArea(new Rect(200, 65, 100, 30), TextArea);

            GUI.Label(new Rect(20, 30, 350, 50), "Current number of scene(s): " + SceneManager.sceneCountInBuildSettings.ToString());

            GUI.Label(new Rect(20, 120, 250, 20), "Keybinds: ");
            GUI.Label(new Rect(20, 140, 250, 20), "K - Get current scene info to Melon Console");
            GUI.Label(new Rect(20, 160, 250, 20), "T - Unlock All Door in scene");
            GUI.Label(new Rect(20, 180, 250, 20), "L - Enable Jumping");
            GUI.Label(new Rect(20, 200, 250, 20), "P - Achievement Giver");
            GUI.Label(new Rect(20, 220, 250, 20), "O - Show hidden Achievemet menu");
            GUI.Label(new Rect(20, 240, 250, 20), "I - Toggle Menu");
            GUI.Label(new Rect(20, 260, 250, 20), "LeftAlt - Toggle Cursor Lock");
            GUI.Label(new Rect(20, 280, 450, 20), "LeftBracket ( [ ) - Remove occlusion culling");
            GUI.Label(new Rect(20, 300, 450, 20), "RightBracket ( ] ) - Open all doors (Use door unlocker first)");
            GUI.Label(new Rect(20, 320, 450, 20), "PageUp/Down - Increase/Decrease movement speed");

            /*For Later Use 
            hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue, 0.0f, 10.0f);
            */

            if (GUI.Button(new Rect(20, 70, 150, 20), "Load Selected Scene"))
            {
                try
                {
                    int temp = int.Parse(TextArea);
                    SceneManager.LoadScene(temp);
                    
                    //For later use
                    //StanleyController.Instance.UnfreezeMotionAndView();
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
                UnlockAllDoors();
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

            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleMenu();
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                ToggleOcclusionCull();
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                OpenAllDoors();
            }

            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                FasterWalkingSpeed();
            }

            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                SlowerWalkingSpeed();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (Cursor.visible == false)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                int sceneCount = SceneManager.sceneCountInBuildSettings;
                LoggerInstance.Msg("Scene Count: " + sceneCount);
                //LoggerInstance.Msg("Scene was loaded. Name: " + S + " int: " + buildIndex);
            }
        }

        public void ToggleMenu()
        {
            menuShown = !menuShown;
        }

        public void FasterWalkingSpeed()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            float speed = 0;
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<StanleyController>() != null)
                {
                    StanleyController PlayerController = go.GetComponent<StanleyController>();
                    speed = PlayerController.WalkingSpeedMultiplier;
                    speed++;
                    PlayerController.SetMovementSpeedMultiplier(speed);
                }
            }

            LoggerInstance.Msg("Speed increased to" + speed);
        }

        public void SlowerWalkingSpeed()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            float speed = 0;
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<StanleyController>() != null)
                {
                    StanleyController PlayerController = go.GetComponent<StanleyController>();
                    speed = PlayerController.WalkingSpeedMultiplier;
                    speed--;
                    PlayerController.SetMovementSpeedMultiplier(speed);
                }
            }

            LoggerInstance.Msg("Speed decreased to" + speed);
        }
        public void OpenAllDoors()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<RotatingDoor>() != null)
                {
                    RotatingDoor Doorscript = go.GetComponent<RotatingDoor>();
                    Doorscript.Input_Open();
                }
            }
            LoggerInstance.Msg("Doors Opened");
        }

        public void ToggleOcclusionCull()
        {
            GameObject Cameraobject = GameObject.Find("Main Camera");
            Camera TheCameraComponent = Cameraobject.GetComponent<Camera>();
            TheCameraComponent.useOcclusionCulling = false;
            TheCameraComponent.farClipPlane = 10000;
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

            LoggerInstance.Msg("Jumping Enabled");
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
            LoggerInstance.Msg("Show Achievment Menu");
        }

        public void UnlockAllDoors()
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
