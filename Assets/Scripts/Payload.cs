// struct Payload
// {
//     public string eventName{get;set;}
//     public Data data{get;set;}
// }
// struct Data
// {
//     public string ticketId{get;set;}
//     public string status{get;set;}
//     public string matchId{get;set;}
// }

sealed class Payload
{
    public string data { get; set; }
    public string eventName { get; set; }
    public string message{get;set;}
    public int messageType{get;set;}
    public int statusCode{get;set;}
    public bool success{get;set;}

    public override string ToString()
    {
        return string.Format("[Event Name: '{0}', data: '<color=yellow>{1}</color>', message: '<color=yellow>{2}</color>'], messageType: '<color=yellow>{3}</color>'], statusCode: '<color=yellow>{4}</color>'], success: '<color=yellow>{5}</color>']", this.eventName, this.data, this.message, this.messageType,this.statusCode,this.success);
    }
}
