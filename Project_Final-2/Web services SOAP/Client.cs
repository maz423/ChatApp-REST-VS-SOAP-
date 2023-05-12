using System;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace Client
{

public class Client {

public static void Main(String[] Args)
{

MathService ws1 = new MathService();




while(true){
Console.WriteLine("Welcome to chatroom: \n 1. create room \n 2. join room  \n 3. List existing rooms  \n 4. exit \n 5. VolumeTest");
String Input = Console.ReadLine();

if(Input == "4"){ //exit room
    Console.WriteLine("Bye");
    Environment.Exit(0);
    break;
}

else if (Input == "1") {//create room
    Console.WriteLine("Please enter the name for the room to be created :");
    String room_name = Console.ReadLine();
    Console.WriteLine(ws1.createRoom(room_name));
}

   else if (Input == "2") {//join room
    
     string grp_joined = Join_room(ws1);

     if(grp_joined.Equals("ERR")){
        Console.WriteLine("\nNo such group exists\n");

     }
     else{
     
     Console.WriteLine("Welcome to room :" + grp_joined +", Enter 'Leave'  to exit room\n");
     Console.WriteLine("Please enter your name: ");
     String user = Console.ReadLine();
     Console.WriteLine(ws1.getMsg(grp_joined));
     Thread thread = new Thread(() => ExecuteInForeground(ws1,grp_joined));
     thread.Start();
     while(true){
     
     Console.WriteLine(">");

     String input_msgs = Console.ReadLine();
     if(input_msgs.Equals("Leave")){
         break;
        }
     else{
         ws1.sendMsg(grp_joined,user,input_msgs);
        

      }

     }
     }

     
     

}

else if (Input == "3") { //list rooms
     Console.Write("Registered rooms :");
     Console.Write(ws1.listRooms());
}

else if(Input == "5"){  //added to send data for experiment.
     Console.Write("1MB\n");
     string text = System.IO.File.ReadAllText(@"./Data1MB.txt");
      var start = DateTime.Now;
      ws1.createRoom(text);
      var end = DateTime.Now;
        
      Console.WriteLine((end - start).ToString());

       


}

else if (Input == "6"){ //added to create rooms for experiment.

 string name = "Room1000.\n";
 int count = 0;
 for (int i = 0;i < 10000; i++){
     ws1.createRoom(name);
     name += "a";
     count += 1;
     Console.Write(count + "\n");

 }

 Console.Write("Done\n");

}





}



}

static string Join_room(MathService ws1){
Console.WriteLine("Enter name of room:");
String grp_name = Console.ReadLine();

// Read the content.
String response = ws1.joinRoom(grp_name);


string str = "OK";

     if(str.Equals(response)){
       return grp_name;

     }
     else{
        return "ERR";
     }





}

  private static void ExecuteInForeground(MathService ws1, string grp_joined)
   {
      
      int prev_count = ws1.getMsgsCount(grp_joined);
     

      while(true){
        
        int new_count = ws1.getMsgsCount(grp_joined);
        
        
        if (new_count > prev_count){
           
           string fetch_msg = ws1.lastMsg(grp_joined);
           Console.Write(fetch_msg + "\n");
           prev_count +=1;
        };
      }
      

   }

}
}
