using DFTGames.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        #region Public Methods

        int currentlanguage;
        private void Start()
        {
            currentlanguage = PlayerPrefs.GetInt("Language", -1);
            switch(currentlanguage)
            {
                case 0: SetEnglish();
                    break;
                case 1: SetGerman();
                    break;
                case 2: SetItalian();
                    break;
                default: SetEnglish();
                    break;
            }
        }

        public void SetEnglish()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.English);
            LocalizeImage.SetCurrentLanguage();
            PlayerPrefs.SetInt("Language", 0);
        }
        public void SetGerman()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.German);
            LocalizeImage.SetCurrentLanguage();
            PlayerPrefs.SetInt("Language", 1);
        }

        public void SetItalian()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.Italian);
            LocalizeImage.SetCurrentLanguage();
            PlayerPrefs.SetInt("Language", 2);
        }

        public void SetJapanese()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.Japanese);
            LocalizeImage.SetCurrentLanguage();
        }

        #endregion Public Methods
    }
}
