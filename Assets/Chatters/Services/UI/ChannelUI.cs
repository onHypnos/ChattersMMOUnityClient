using System;
using System.Collections.Generic;
using Lexone.UnityTwitchChat;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatters.Services.UI
{
    public class ChannelUI : MonoBehaviour
    {
        [SerializeField]private TMP_InputField _input;
        [SerializeField] private Button _confirm;
        [SerializeField] private Button _disconnect;
        [SerializeField] private Image _statusImage;
        [Header("StatusImages")]
        [SerializeField] private Color _statusOK = new(47,245,47,255);
        [SerializeField] private Color _statusPending = new(245,240,24,255);
        [SerializeField] private Color _statusError = new(150,40,0,255);
        [SerializeField] private Color _statusDisconnected = Color.white;

        [Header("DeletingButton")] 
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Transform _deleteConfirmationPanel;
        [SerializeField] private Button _deleteDeny;
        [SerializeField] private Button _deleteConfirm;
        
        private Dictionary<ConnectionStatus,Color> _statuses;

        public Func<string,string> OnConnectClicked;
        public Action OnDisconnectClicked;
        public event Action OnDeleteRequest;
        public void Init()
        {
            SetDeleteConfirmationCheckStatus(false);
            InitStatus();
            _confirm.onClick.AddListener(Connect);
            _disconnect.onClick.AddListener(Disconnect);
            _deleteButton.onClick.AddListener(() => SetDeleteConfirmationCheckStatus(true));
            _deleteDeny.onClick.AddListener(() =>SetDeleteConfirmationCheckStatus(false));
            _deleteConfirm.onClick.AddListener(() => OnDeleteRequest?.Invoke());
        }

        private void SetDeleteConfirmationCheckStatus(bool value)
        {
            _deleteConfirmationPanel.gameObject.SetActive(value);
        }

        private void Connect()
        {
            _confirm.gameObject.SetActive(false);
            _statusImage.color = _statusPending;
            var result = OnConnectClicked?.Invoke(_input.text);
            _input.text = result;
        }

        private void Disconnect()
        {
            _confirm.gameObject.SetActive(true);
            _statusImage.color = _statusDisconnected;
            OnDisconnectClicked?.Invoke();
        }

        private void InitStatus()
        {
            _statuses = new();
            _statuses.Add(ConnectionStatus.OK,_statusOK);
            _statuses.Add(ConnectionStatus.PENDING,_statusPending);
            _statuses.Add(ConnectionStatus.ERROR,_statusError);
            _statuses.Add(ConnectionStatus.DISCONNECTED,_statusDisconnected);
        }
        
        public void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public Action OnDestroyed;

        public enum ConnectionStatus
        {
            DISCONNECTED,
            ERROR,
            PENDING,
            OK
        }
        
        public void OnConnectionAlert(IRCReply obj)
        {
            var color = _statusDisconnected;
            switch (obj)
            {
                case IRCReply.CONNECTED_TO_SERVER:
                    color = _statusPending;
                    break;
                case IRCReply.PONG_RECEIVED:
                    break;
                case IRCReply.JOINED_CHANNEL:
                    color = _statusOK;
                    break;
                case IRCReply.MISSING_LOGIN_INFO:
                    break;
                case IRCReply.BAD_LOGIN:
                    color = _statusError;
                    break;
                case IRCReply.CONNECTION_INTERRUPTED:
                    color = _statusError;
                    break;
                case IRCReply.NO_CONNECTION:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }

            _statusImage.color = color;
        }
    }
}