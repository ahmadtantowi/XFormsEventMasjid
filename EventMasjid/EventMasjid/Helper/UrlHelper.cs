using System;
using System.Collections.Generic;
using System.Text;

namespace EventMasjid.Helper
{
    class UrlHelper
    {
        private const string BASE_URL = "https://eventmasjid.azurewebsites.net/index.php/";

        public const string LOGIN_URL = BASE_URL + "auth/login/";

        public const string DKM_URL = BASE_URL + "Dkm";

        public const string DKM_URL_LOGIN = BASE_URL + "Dkm?Uname_Dkm={0}&Pass_Dkm={1}";

        public const string DKM_URL_EVENT= BASE_URL + "Dkm?Uname_Dkm={0}";

        public const string DKM_URL_CRUD = BASE_URL + "Dkm/{0}";

        public const string EVENT_URL = BASE_URL + "Pull";

        public const string EVENT_URL_CRUD = BASE_URL + "Pull/{0}";
    }
}
