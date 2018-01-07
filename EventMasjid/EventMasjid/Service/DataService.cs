using EventMasjid.Helper;
using EventMasjid.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EventMasjid.Service
{
    class DataService
    {
        public async Task<bool> Login(string username, string password)
        {
            var uriLogin = new Uri(string.Format(UrlHelper.DKM_URL_LOGIN, username, password));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    var respone = await client.GetAsync(uriLogin);
                    Debug.WriteLine(uriLogin.ToString());
                    Debug.WriteLine(respone.ToString());

                    if (!respone.IsSuccessStatusCode)
                        return false;

                    var byteResult = await respone.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    var modelResult = JsonConvert.DeserializeObject<List<Event>>(result);
                    Debug.WriteLine(result.ToString());

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return false;
                }
            }

            ////add
            //object userInfos = new { uname = username, pword = password };
            //var jsonObj = JsonConvert.SerializeObject(userInfos);
            //using (HttpClient client = new HttpClient())
            //{
            //    StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
            //    var request = new HttpRequestMessage()
            //    {
            //        RequestUri = new Uri(UrlHelper.DKM_URL_LOGIN),
            //        Method = HttpMethod.Post,
            //        Content = content
            //    };
            //    //you can add headers                
            //    //request.Headers.Add("Client-Services", "fortend-client");
            //    //request.Headers.Add("Auth-Key", "mapro");

            //    var response = await client.SendAsync(request);
            //    Debug.WriteLine(response.ToString());
            //    if (!response.IsSuccessStatusCode)
            //        return false;
            //    return true;

            //    //string dataResult = response.Content.ReadAsStringAsync().Result;
            //    //bool result = JsonConvert.DeserializeObject<bool>(dataResult);
            //    //return result;
            //}

            //using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            //{
            //    try
            //    {
            //        var loginDict = new Dictionary<string, string> { { "username", username }, { "password", password } };
            //        var postData = JsonConvert.SerializeObject(loginDict);
            //        HttpContent stringContent = new StringContent(postData);
            //        stringContent.Headers.Add("Client-Services", "fortend-client");
            //        stringContent.Headers.Add("Auth-Key", "mapro");
            //        stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            //        var builder = new UriBuilder(new Uri(UrlHelper.LOGIN_URL));
            //        var respone = await client.PostAsync(builder.Uri, stringContent);
            //        Debug.WriteLine(respone.ToString());

            //        if (!respone.IsSuccessStatusCode)
            //            return false;

            //        //var byteResult = await respone.Content.ReadAsByteArrayAsync();
            //        //var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

            //        //if (!result.Equals("Successfully login.", StringComparison.OrdinalIgnoreCase))
            //        //    return false;

            //        return true;
            //    }
            //    catch (Exception exc)
            //    {
            //        return false;
            //    }
            //}
        }

        public async Task<List<Dkm>> GetListDkm()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    //Accept application/json only
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.DKM_URL));
                    var response = await client.GetAsync(builder.Uri);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    var modelResult = JsonConvert.DeserializeObject<List<Dkm>>(result);


                    return modelResult;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetListEvent()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    //Accept application/json only
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.EVENT_URL));
                    var response = await client.GetAsync(builder.Uri);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    var modelResult = JsonConvert.DeserializeObject<List<Event>>(result);

                    Debug.WriteLine(result.ToString());

                    return modelResult;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Service untuk menyimpan data Event Masjid dengan method POST dan PUT
        /// </summary>
        /// <param name="events">data yang akan disimpan</param>
        /// <param name="isNewEvent">true:POST; false:PUT</param>
        /// <returns></returns>
        public async Task<bool> SaveEvent(Event events, bool isNewEvent = false)
        {
            var uriPost = new Uri(string.Format(UrlHelper.EVENT_URL_CRUD, string.Empty));
            var uriPut = new Uri(string.Format(UrlHelper.EVENT_URL_CRUD, events.Id_Event));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    var post = JsonConvert.SerializeObject(events);
                    var content = new StringContent(post, Encoding.UTF8, "application/json");

                    Debug.WriteLine(post.ToString());

                    HttpResponseMessage respone = null;
                    if(isNewEvent)
                        respone = await client.PostAsync(uriPost, content);
                    else
                        respone = await client.PutAsync(uriPut,content);

                    if (!respone.IsSuccessStatusCode)
                        return false;

                    return true;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Service untuk menghapus data Event Masjid
        /// </summary>
        /// <param name="idEvent">ID data yang akan dihapus</param>
        /// <returns></returns>
        public async Task<bool> DeleteEvent(string idEvent)
        {
            var uriDelete = new Uri(string.Format(UrlHelper.EVENT_URL_CRUD, idEvent));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    var respone = await client.DeleteAsync(uriDelete);

                    if (!respone.IsSuccessStatusCode)
                        return false;

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> SaveDkm(Dkm dkm, bool isNewDkm = false)
        {
            var uriPost = new Uri(string.Format(UrlHelper.DKM_URL_CRUD, string.Empty));
            var uriPut = new Uri(string.Format(UrlHelper.DKM_URL_CRUD, dkm.Id_Dkm));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    var post = JsonConvert.SerializeObject(dkm);
                    var content = new StringContent(post, Encoding.UTF8, "application/json");
                    //HttpContent content = new StringContent(post);
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    HttpResponseMessage respone = null;
                    if (isNewDkm)
                        respone = await client.PostAsync(uriPost, content);
                    else
                        respone = await client.PutAsync(uriPut, content);

                    Debug.WriteLine(content.ToString());
                    Debug.WriteLine(respone.ToString());

                    if (!respone.IsSuccessStatusCode)
                        return false;

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Kesalahan {0}", ex.Message);
                    return false;
                }
            }
        }
    }
}
