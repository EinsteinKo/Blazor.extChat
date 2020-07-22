using System;
using System.Collections.Generic;
using System.Text;

//using ClassLibrary3;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlazorApp3.Shared
{
    public class Library
    {
    }


    public class FormsModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Require SKCode or User.ID")]
        public string User { get; set; }
        public string GroupString { get; set; }
        public DateTime Date { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Oh noes! that name is already taken")]
        public string InValid { get; set; }
        public string Blank { get; set; }
        public string Message { get; set; }
        public List<string> OtherResult { get; set; }
        public string NotSee { get; set; }

        //public string Email { get; set; } = "email@example.com";
    }

    public class ChatMsgList
    {
        public int id { get; set; }
        public DateTime MsgTime { get; set; }
        public string user { get; set; }
        public string msg { get; set; }
        public string notsee { get; set; }
    }


    public class ChatUser
	{
        public DateTime JoinTime { get; set; }
        public string user { get; set; }
	}

}
