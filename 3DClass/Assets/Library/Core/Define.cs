using UnityEngine;
namespace Core
{
    public enum StateType
    {
        Normal = 0,
        Attack = 1,
        //OnHit = 2,
        Rolling = 3,
    }

    public enum ResourceType
    {
        HealOrb,
    }

    public class Define
    {
        private static Camera mainCam = null;
        public static Camera MainCam
        {
            get
            {
                if(mainCam == null)
                {
                    mainCam = Camera.main;
                }
                return mainCam;
            }
        }
    }
}