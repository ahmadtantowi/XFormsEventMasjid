using System;
using System.Collections.Generic;
using System.Text;

namespace EventMasjid.Helper
{
    class UrlHelper
    {
        private const string BASE_URL = "https://eventmasjid.azurewebsites.net/index.php/";

        public const string LOGIN_URL = BASE_URL + "auth/login";

        public const string DKM_URL = BASE_URL + "dkm";

        public const string DKM_URL_LOGIN = BASE_URL + "dkm?uname_dkm={0}&pass_dkm={1}";

        public const string DKM_URL_CRUD = BASE_URL + "dkm/{0}";

        public const string EVENT_URL = BASE_URL + "pull";

        public const string EVENT_URL_CRUD = BASE_URL + "pull/{0}";
    }
}
