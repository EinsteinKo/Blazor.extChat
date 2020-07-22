using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

using BlazorApp3.Shared;
using BlazorApp3.Server.Controllers;
using System.Security.Cryptography.X509Certificates;
//using ClassLibrary3;

namespace BlazorApp3.Server.Hubs
{
	public class ChatHub :Hub
	{
 

        public ChatHub()
        {
            //_context = context;
        }

        // Chat.User code 
        public static List<ChatUser> userList = new List<ChatUser>();
        private static List<ChatMsgList> originMsgList = new List<ChatMsgList>();
        public void AddUser(ChatUser user)
        {
            userList.Add(user);
        }

        public string GetUserList()
        {
            string res = string.Join(",", userList.Select(x => x.user));
            return res;
        }

        public bool UserCheck(string user)
        {
            return userList.Any(x => x.user == user);
        }

        // msg code....
        public async Task SendMessage(FormsModel msg)
        {
            msg.Blank = "yes";
            msg.Date = DateTime.Now;

            if (!UserCheck(msg.User))
            {
                AddUser(new ChatUser()
                {
                    JoinTime = DateTime.Now,
                    user = msg.User,                    
                });
            }

          msg.NotSee = GetUserList();
            originMsgList.Add(new ChatMsgList()
            {
                MsgTime = msg.Date,
                msg = msg.Message,
                user = msg.User,
                notsee = msg.NotSee
            });

            await Clients.All.SendAsync("ReceiveMessage", msg);
        }

        // 읽은 메세지 처리 후 재 전송......!!!!
        public async Task LastMessageSee(string user)
		{
            foreach(var item in originMsgList)
			{
                item.notsee = SeenUserRemove(item.notsee, user);             
            }
            await Clients.All.SendAsync("LastMessageSeePrs", originMsgList);
        }

        private string SeenUserRemove(string origin, string user)
		{
			string result = origin.Replace(user + ",", "");
			result = result.Replace(","+user, "");
            result = result.Replace(user, "");
            return result;
		}

    }

}
