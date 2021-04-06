using UnityEngine;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using UnityEngine.Networking;
using System;


public class H2 : MonoBehaviour
{
    Uri uri = new Uri("https://localhost:44337");
    const string hubUri = "/hubs/multiplayer-lobby";
    string user = "admin";
    string currentTicketId = "";
    string currentMatchId = "";

    public void OnClickConnectButton()
    {
        //  + "?userId="+user+"&appContext=multiplayer-lobby"
        
            // .withUrl(`${baseUri}${hubUri}?userId=${user}&appContext=multiplayer-lobby`, {
            //     skipNegotiation: true,
            //     transport: signalR.HttpTransportType.WebSockets
            // })
            // .build();
        Uri url = new Uri(uri.OriginalString + hubUri + "?userId=" + user /*+ "&appContext=multiplayer-lobby" */ );
        Debug.Log(url.OriginalString);
        
        HubOptions options = new HubOptions();
        options.SkipNegotiation = true;
        options.PreferedTransport = TransportTypes.WebSocket;
        

        IProtocol protocol = new JsonProtocol(new LitJsonEncoder());
        HubConnection hub = new HubConnection(url, protocol, options);
        RegisterEvents(hub);

        hub.OnConnected += OnHubConnection;
        hub.OnError += OnHubError;
        hub.OnClosed += OnHubClosed;

        Debug.Log("Wait For it...");
        hub.StartConnect();

    }
    void OnHubConnection(HubConnection hub){
        Debug.Log("CONNECTED");

        
    }
    void RegisterEvents(HubConnection hub){
        //Register Events
        hub.On<string, Payload>("LogStreamEvent", (userId,payload) => {
            Debug.Log("userId: " + userId + " && payload: " + payload);
        });


        hub.On<Payload>("RealtimeEvent", (payload) => {
            switch (payload.eventName) {

                case "GetaLobbyConnected":
                    Debug.Log("[GetaLobbyConnected]: "+ payload.ToString());
                break;

                // case "RequestMatchmaking::TicketResponse":
                //     Debug.Log("[TicketResponse] "+ payload);
                //     currentTicketId = payload.data.ticketId;

                //     //Coroutine waitforxSeconds
                //     RequestTicketStatusInterval = window.setInterval(_ => {
                //         GetTicketStatus();
                //     }, 6000);

                // break;

                // // Return the match status fired on microservices, pay attention in Status property, the possible values was
                // // - waitingForMatch
                // // - Cancelled: Timeout
                // // - Matched: Contains a list of members
                // case "MatchmakingPolling::MatchStatus":
                //     Debug.Log("[MatchmakingPolling::MatchStatus]" + payload.data);

                //     if (payload.data.status.ToLower() == "matched") {
                //         currentMatchId = payload.data.matchId;
                //         clearInterval(RequestTicketStatusInterval);

                //         //Join to TeamGroup
                //         RequestJoinTeamGroup();
                //     }

                //     else if (payload.data.status.toLowerCase() == "canceled") {
                //         clearInterval(RequestTicketStatusInterval);
                //     }
                // break;

                // // Return the response when join in a group with other player
                // case "JoinTeamGroup::Response":
                //     var usersInGroup = JSON.parse(payload.data.playersInGroup);
                //     usersInGroup.forEach(userInGroup => {
                //         if (userInGroup.UserId == user) {
                //             isLeader = userInGroup.IsLeader;
                //         }
                //     });
                //     Debug.Log("[JoinTeamGroup::Response]", payload);
                //     Debug.Log("IsLeader: ", isLeader);
                // break;

                // Return the server information to create a multiplayer connection with URI and port
                // case "ServerConnection::ServerInformation":
                //     Debug.Log("[ServerInformation]" + payload.matchId);
                // break;

                // // Return ServerConnection error
                // case "ServerConnection::Error":
                //     Debug.Log("RealtimeEvent: [${payload.eventName}]", payload.message);

                //     //Check if error is noServersAvailable and user isLeader of matchGroup
                //     if (payload.message.toLowerCase().indexOf("nohostsavailableinregion") != -1 && isLeader) {
                //         var payload = {
                //             TitleId: titleId,
                //             BuildId: buildId,
                //             MatchId: currentMatchId,
                //             UserId: user,
                //             ApiDelay: apiDelay,
                //             MaxServerAttempts: maxServerAttempts
                //         }

                //         Debug.Log("Request new server from Playfab");
                //         connection.invoke("RequestPlayfabServer", JSON.stringify(payload)).catch(function (err) {
                //             return console.error(err.toString());
                //         });
                //     }
                // break;

                // // Is an standard error notifying when anything was wrong, the payload has all information about error
                // case "RequestMatchmaking::Error":
                // case "MatchmakingPolling::Error":
                // case "JoinTeamGroup::Error":
                // case "FightGroup::Error":
                // case "RequestLeaderServerInformation::Error":
                //     Debug.Log(`RealtimeEvent: [${payload.eventName}]`, payload.message);
                // break;

                default:
                    Debug.Log("RealtimeEvent: "+ payload);
                break;
            }
        });
    }
    void OnHubError(HubConnection hub, string error){
        Debug.Log("ERROR");
    }
    void OnHubClosed(HubConnection hub){
        Debug.Log("CLOSED");
    }
}
