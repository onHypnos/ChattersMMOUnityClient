using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Services.UI
{
    public class NicknameExample : MonoBehaviour
    {
        public TMP_Text Text;
        public Color DefaultColor = Color.white;


        public void Init(string nickName)
        {
            Init(nickName, DefaultColor);
        }

        public void Init(string nickName, Color color)
        {
            Text.text = nickName;
            Text.color = color;
        }
    }
}