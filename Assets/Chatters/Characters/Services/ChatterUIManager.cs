using System.Collections;
using Chatters.Characters.Mediators;
using TMPro;
using UnityEngine;

namespace Chatters.Characters.Services
{
    public class ChatterUIManager : CharacterService
    {
        public TMP_Text NickName;
        public TMP_Text MessageBoxText;
        public Transform MessageBox;
        private Vector3 _startLocalBoxPosition;
        private readonly Vector3 _comingUpDelta = Vector3.up * 0.01f;
        
        public Coroutine ShowingMessageCoroutine;
        
        public override void Init(BaseMediator.ServiceContainer serviceContainer)
        {
            base.Init(serviceContainer);
            MessageBox.gameObject.SetActive(false);
            _startLocalBoxPosition = MessageBox.localPosition;
        }

        public void ChangeNickname(string nickname)
        {
            NickName.text = nickname;
            NickName.transform.localPosition += new Vector3(0, Random.Range(0, 1f), 0);
        }

        public void ChangeColor(bool random = true)
        {
            if (random)
            {
                ChangeColor(Random.ColorHSV());
            }
        }

        public void ChangeColor(Color color)
        {
            NickName.color = color;
        }
        
        public void ShowMessage(string message)
        {
            if(ShowingMessageCoroutine!=null)
                StopCoroutine(ShowingMessageCoroutine);
            ShowingMessageCoroutine = StartCoroutine(ShowingMessage(message));
        }
        
        private IEnumerator ShowingMessage(string message)
        {
            MessageBox.localPosition = _startLocalBoxPosition;
            MessageBoxText.text = message;
            MessageBox.gameObject.SetActive(true);
            
            yield return new WaitForSecondsRealtime(0.5f);
            for (int i = 0; i < 240; i++)
            {
                MessageBox.localPosition += _comingUpDelta;
                yield return new WaitForFixedUpdate();
            }
            
            MessageBox.gameObject.SetActive(false);
        }
    }
}