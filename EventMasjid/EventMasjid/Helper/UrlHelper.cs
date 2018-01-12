using System;
using System.Collections.Generic;
using System.Text;

namespace EventMasjid.Helper
{
    class UrlHelper
    {
        private const string BASE_URL = "https://eventmasjid.azurewebsites.net/index.php/";

        public const string LOGIN_URL = BASE_URL + "auth/login/";

        public const string EDIT_URL = BASE_URL + "Edit";

        public const string DELETE_URL = BASE_URL + "Delete";

        public const string DKM_URL = BASE_URL + "Dkm";

        public const string DKM_URL_LOGIN = BASE_URL + "Dkm?Uname_Dkm={0}&Pass_Dkm={1}";

        public const string DKM_URL_EVENT= BASE_URL + "Dkm?Uname_Dkm={0}";

        public const string EVENT_URL = BASE_URL + "Pull";
    }
}
