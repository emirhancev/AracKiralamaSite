using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string SuccessfullyAdded = "Successfully added";
        public static string SuccessfullyUpdated = "Successfully updated";
        public static string MailAlreadyInUser = "Email already in use";
        public static string SuccessfullyDeleted = "Successfully deleted";
        public static string ObjectSuccessfullyReturned = "Object successfully returned";
        public static string InvalidDailyPrice = "Error : Invalid daily price - Daily price cannot be lower than 0";
        public static string InvalidModelYear = "Error : Invalid model year - Model year cannot be lower than 1769 and higher than 32767";
        public static string ObjectNotFound = "Error : Object not found";
    }
}
