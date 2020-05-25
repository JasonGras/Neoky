using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class Constants
    {
        public const string SCENE_NOSCENE = "NOSCENE";   
        public const string SCENE_SAMESCENE = "SAMESCENE";   
        public const string SCENE_HOMEPAGE = "HomePage";   
        public const string SCENE_COLLECTION = "CollectionPage";   
        public const string SCENE_LOADING = "LoadingScene";   
        public const string SCENE_SIGNUP = "SignUp";   
        public const string SCENE_AUTHENTICATION = "Authentication";   
        public const string SCENE_MAINTENANCE = "Maintenance"; 
        public const string SCENE_REDEFINEPASSWORD = "ChangePassword"; 

        /*Error Label_id*/

        public const string error_email_format_lbl = "error_email_format_lbl";  // SignUp
        public const string error_username_format_lbl = "error_username_format_lbl";
        public const string error_password_format_lbl = "error_password_format_lbl";
        public const string error_confirmed_password_format_lbl = "error_confirmed_password_format_lbl";
        public const string signup_validation_email_lbl = "signup_validation_email_lbl";
        public const string authentication_failed_lbl = "authentication_failed_lbl";
        public const string signup_failed_lbl = "signup_failed_lbl";
        public const string technical_error_lbl = "technical_error_lbl";
        public const string authentication_non_confirmed_user_lbl = "authentication_non_confirmed_user_lbl";
        public const string text_level_lbl = "text_level_lbl";
        public const string sign_up_user_already_exist = "sign_up_user_already_exist";


    }
}
