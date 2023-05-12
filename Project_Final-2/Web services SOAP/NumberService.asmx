<%@ WebService Language="C#" Class="MathService.MathService" %>
using System;
using System.Web.Services;
using System.Collections;
namespace MathService
{


[WebService (Namespace = "http://tempuri.org/NumberService")]
public class MathService : WebService
{

static Hashtable HT = new Hashtable();  




[WebMethod]
public int AddNumbers (int number1, int number2)
{
return number1 + number2;
}

[WebMethod]
public string createRoom (string name)
{
 if(HT.ContainsKey(name) == false){ 
 ArrayList ar = new ArrayList(); 
 HT.Add(name, ar);   
 return "room created";
 }
 else{
    return "oops! room already exists";}
}

[WebMethod]
public string joinRoom (string name)
{
 if(HT.ContainsKey(name)){
    return "OK";
 }
 else{
return "ERR";
 }
 
}

[WebMethod]
public string sendMsg (string name,string user, string msg)
{
   if(HT.ContainsKey(name)){
       ArrayList room = (ArrayList)HT[name];
       room.Add(user + " : " + msg);
       return user + " : " + msg + "\n";
   }
   else{
      return "ERR";
   }
 
}



[WebMethod]
public string listRooms ()
{
    string res = "";
    ArrayList al = new ArrayList(HT.Keys) ; 
    for(int i = 0; i < al.Count; i++){  
         res += al[i] + "\n";
    }  
    return res;

}

[WebMethod]
public string getMsg (string name)
{
 if(HT.ContainsKey(name)){
     ArrayList msgs = (ArrayList)HT[name];
     string res = "";
     for(int i = 0; i< msgs.Count;i++ ){
        res += msgs[i] + "\n" ;
     }
   return res;

 }
 else{
   return "ERR";
 }
}


[WebMethod]
public string lastMsg (string name)
{
 if(HT.ContainsKey(name)){
     ArrayList msgs = (ArrayList)HT[name];
    
     if(msgs.Count != 0){
   return msgs[msgs.Count -1].ToString();
   }
   else{return "";}

 }
 else{
   return "";
 }
}

[WebMethod]
public int getMsgsCount (string name)
{
 if(HT.ContainsKey(name)){
     ArrayList msgs = (ArrayList)HT[name];
     
   return msgs.Count;

 }
 else{
   return -1;
 }
}





[WebMethod]
public int SubtractNumbers (int number1, int number2)
{
return number1 - number2;
}
}
}
