using System;
using System.IO;
using System.Net;
using System.Threading;




namespace Client
{
public class Client
{
public static void Main()
{

while (true)
{
    

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/");
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

// Get the stream containing content returned by the server.
// The using block ensures the stream is automatically closed.
using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);
String Input = Console.ReadLine();

if(Input == "4"){ //exit room
    Console.WriteLine("Bye");
    break;
}
else if (Input == "1") {//create room
    Console.WriteLine("Please enter the name for the room to be created :");
    String room_name = Console.ReadLine();
    create_room(room_name);
}
else if (Input == "2") {//join room
     Console.WriteLine("Join");
     string grp_joined = Join_room();

     if(grp_joined.Equals("ERR")){
        Console.WriteLine("\nNo such group exists\n");

     }
     else{
     
     Console.WriteLine("Welcome to room :" + grp_joined +", Enter 'Leave'  to exit room\n");
    
     Console.WriteLine("Please enter your name: ");
     String user = Console.ReadLine();
     get_msgs(grp_joined);

     Thread thread = new Thread(() => ExecuteInForeground(grp_joined));
     thread.Start();
     while(true){
     
     Console.WriteLine(">");

     String input_msgs = Console.ReadLine();
     if(input_msgs.Equals("Leave")){
         break;
        }
     else{
         Send_msgs(grp_joined,input_msgs,user);
         // get_msg(grp_joined);

      }

     }}

     
     

}
else if (Input == "3") { //list rooms
     List_rooms();
}

else if(Input == "5"){ //added to send experiment data (Data volumes test.)


     //string text = System.IO.File.ReadAllText(@"Amaan");
      var start = DateTime.Now;
      create_room("Amaan");
      var end = DateTime.Now;
        
       Console.WriteLine((end - start).ToString());


}
else if (Input == "6"){  //added to send experiment data (adding large number of rooms to the system).

 string name = "Room";
 for (int i = 0;i < 10000; i++){
     create_room(name);
     name += "a";

 }

}



}
// Close the response.
response.Close();
}


}


/*
Creates a chatroom.
*/

static void create_room(String name){
// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/create?name="+name);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);


}

}


/*
Checks if a room exists or not.
Return: group name if exists , ERR otherwise.
*/


static string Join_room(){
Console.WriteLine("Enter name of room:");
String grp_name = Console.ReadLine();
WebRequest request = WebRequest.Create("http://python_1:5000/join?name="+ grp_name);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd();


string str = "OK";

     if(str.Equals(responseFromServer)){
       return grp_name;

     }
     else{
        return "ERR";
     }


}


}

/*
Lists all chatrooms.
*/

static void List_rooms(){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/list");
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);


}

}

/*
posts message to a chatroom that has the given name.
*/


static void Send_msgs(string grp, string message,string user){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/send?name="+ grp + "&msg="+ message + "&user=" + user);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.

string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);




}

}

/*
gets the last message sent by the user to the room
*/


static string get_msg(string grp){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/getMsg?name="+grp);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.

string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);
return responseFromServer;



}

}

/*
gets all the  messages sent to the room
*/

static void get_msgs(string grp){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/getMsgs?name="+grp);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.

string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);




}

}

/*
created this method to send large data volumes.
*/
static void sendData(String data){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/create?name="+ data);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
string responseFromServer = reader.ReadToEnd();
// Display the content.
Console.WriteLine(responseFromServer);


}

}

/*
gets a count of messages in a room
*/
static int getCount(String grp){

// Create a request for the URL.
WebRequest request = WebRequest.Create("http://python_1:5000/count?name="+ grp);
// If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;

// Get the response.
WebResponse response = request.GetResponse();

using (Stream dataStream = response.GetResponseStream())
{
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
// Read the content.
int responseFromServer = Int32.Parse(reader.ReadToEnd());
// Display the content.

return responseFromServer;

}

}



/*
thread listens to messages to get new msgs as they are posted
*/
private static void ExecuteInForeground(string grp_joined)
   {
      
      int prev_count = getCount(grp_joined);
     

      while(true){
        
        int new_count = getCount(grp_joined);
        
        
        if (new_count > prev_count){
           
           string fetch_msg = get_msg(grp_joined);
         
           prev_count +=1;
        };
      }
      

   }




}



}



