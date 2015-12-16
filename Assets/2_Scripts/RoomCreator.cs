using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    /// <summary>
    /// Creates a custom room
    /// </summary>
    public void CreateCustomRoom()
    {
        //Classic room options
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.isVisible = true;   //Should the room be available to display ?
        roomOptions.maxPlayers = 10;    //Max number of players for a single room

        //Custom room options
        roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOptions.customRoomProperties.Add(RoomProperty.DisplayName, "RoomPlaceHolder");  //Name to display in room browser
        roomOptions.customRoomProperties.Add(RoomProperty.Map, "MapPlaceholder");           //Map of the room
        roomOptions.customRoomProperties.Add(RoomProperty.HostName, "HostPlaceHolder");     //Name of the original host

        roomOptions.customRoomPropertiesForLobby = new string[]
        {
            RoomProperty.DisplayName,
            RoomProperty.Map,
            RoomProperty.HostName
        };

        //Creates a room -> the weird part is the real room name for connecting (Using a GUID because every room name must differ)
        PhotonNetwork.CreateRoom("Room" + System.Guid.NewGuid().ToString("N"), roomOptions, TypedLobby.Default);
    }

    /// <summary>
    /// Triggers when a room is joined/created 
    /// </summary>
    void OnJoinedRoom()
    {
        //Load the scene equivalent to the room's map when the room is joined/created
        PhotonNetwork.LoadLevel(PhotonNetwork.room.customProperties[RoomProperty.Map].ToString());
    }

}
