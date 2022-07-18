using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{

    // Game Version
    private readonly string version = "1.0";

    // Username : DisplayName
    private string userId = "피자땡겨";

    void Awake()
    {
        // 마스터 클라이언트의 씬 자동 동기화 옵션
        // 해당 룸의 마스터가 새로운 씬을 로딩했을 때 다른 접속 유저들에게도 같은 씬을 로딩해주는 기능
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임 버전 설정
        // 같은 버전의 유저들끼리만 접속을 허용하는 기능
        PhotonNetwork.GameVersion = version;

        // 접속 유저의 닉네임 설정
        PhotonNetwork.NickName = userId;

        // 포톤 서버와의 데이터 초당 전송 횟수
        // 기본은 초당 30회로 되어있
        Debug.Log(PhotonNetwork.SendRate);


        // 포톤 서버 접속
        //PhotonNetwork.ConnectUsingSettings();

    }

    public void FindMatch()
    {
        Debug.Log("Check");
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.ConnectUsingSettings();
    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
        //base.OnConnectedToMaster();
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        //PhotonNetwork.JoinRandomOrCreateRoom

        PhotonNetwork.JoinRandomRoom();

        //base.OnJoinedLobby();
    }

    // 랜덤한 룸 입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}:{message}");


        // 룸의 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;      // 룸에 접속할 수 있는 최대 접속자 수 : PVP, 1대1이므로
        ro.IsOpen = true;       // 룸의 오픈 여부
        ro.IsVisible = true;    // 로비에서 룸 목록에 노출시킬지 여부

        // 룸 생성
        // 룸을 생성한 유저는 자동으로 해당 룸에 입장 -> OnJoinedRoom 콜백 함수 호출
        PhotonNetwork.CreateRoom("My room", ro);

        //base.OnJoinRandomFailed(returnCode, message);
    }

    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
        //base.OnCreatedRoom();
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            // NickName은 고유하지 않음, ActorNumber는 고유함
            Debug.Log($"{player.Value.NickName} , {player.Value.ActorNumber}");
        }
        //base.OnJoinedRoom();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
