using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class LoggedInUser
    {
        private static int Id;
        

        public LoggedInUser(int id)
        {
            Id = id;
        }


        public static int GetUserID()
        {
            return Id;
        }
    }
}