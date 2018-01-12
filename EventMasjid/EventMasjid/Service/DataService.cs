using EventMasjid.Helper;
using EventMasjid.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.Settings;
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

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    var respone = await client.GetAsync(uriLogin);

                    if (!respone.IsSuccessStatusCode)
                        return false;

                    CrossSettings.Current.AddOrUpdateValue("uname", username);
                    CrossSettings.Current.AddOrUpdateValue("pass", password);

                    Debug.WriteLine("URI login: " + uriLogin.ToString());
                    Debug.WriteLine("Respon service: " + respone.ToString());

                    await GetMyDkm();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return false;
                }
            }
        }

        public async Task<string> GetMyDkm()
        {
            var uriMyDkm = new Uri(string.Format(UrlHelper.DKM_URL_LOGIN, CrossSettings.Current.GetValueOrDefault("uname", null), CrossSettings.Current.GetValueOrDefault("pass", null)));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) })
            {
                try
                {
                    var respone = await client.GetAsync(uriMyDkm);
                    if (!respone.IsSuccessStatusCode)
                        return "Rincian DKM gagal diambil, silakan Segarkan halaman!";

                    var byteResult = await respone.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<Dkm>>(result);
                    Dkm myDkm = modelResult[0];

                    //jangan ditiru, coba pelajari lagi desgin pattern MVVM
                    MyDkm.Id_Dkm = myDkm.Id_Dkm;
                    MyDkm.Uname_Dkm = myDkm.Uname_Dkm;
                    MyDkm.Pass_Dkm = myDkm.Pass_Dkm;
                    MyDkm.Alamat_Dkm = myDkm.Alamat_Dkm;
                    MyDkm.Tlp_Dkm = myDkm.Tlp_Dkm;
                    MyDkm.Email_Dkm = myDkm.Email_Dkm;
                    MyDkm.Ketua_Dkm = myDkm.Ketua_Dkm;
                    MyDkm.Masjid_Dkm = myDkm.Masjid_Dkm;

                    return null;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return "Terjadi kesalahan, silakan Segarkan halaman!";
                }
            }
        }

        public async Task<List<Dkm>> GetListDkm()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
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
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetListEvent()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
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
                    Debug.WriteLine(result.ToString());
                    var modelResult = JsonConvert.DeserializeObject<List<Event>>(result);

                    Debug.WriteLine("URI List Event" + builder.ToString());

                    return modelResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetMyEvent()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    //Accept application/json only
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(string.Format(UrlHelper.DKM_URL_EVENT, MyDkm.Uname_Dkm));
                    var response = await client.GetAsync(builder.Uri);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    var modelResult = JsonConvert.DeserializeObject<List<Event>>(result);

                    Debug.WriteLine("URI My Event: " + builder.ToString());

                    return modelResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"Kesalahan {0}", ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Service untuk menyimpan data Event Masjid dengan method POST dan PUT
        /// </summary>
        /// <param name="events">data event yang akan disimpan</param>
        /// <param name="isNewEvent">true: buat baru; false: update</param>
        /// <returns></returns>
        public async Task<bool> SaveEvent(Event events, bool isNewEvent = false)
        {
            var uriCreate = new Uri(string.Format(UrlHelper.EVENT_URL));
            var uriUpdate = new Uri(string.Format(UrlHelper.EDIT_URL));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    var post = JsonConvert.SerializeObject(events);
                    var content = new StringContent(post, Encoding.UTF8, "application/json");

                    Debug.WriteLine(post.ToString());

                    HttpResponseMessage respone = null;
                    if(isNewEvent)
                        respone = await client.PostAsync(uriCreate, content);
                    else
                        respone = await client.PostAsync(uriUpdate,content);

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
        /// Service untuk menghapus data DKM atau Event
        /// </summary>
        /// <param name="id">id data yang ingin dihapus</param>
        /// <param name="isDkm">true: data DKM; false: data Event</param>
        /// <returns></returns>
        public async Task<bool> DeleteDkmEvent(string id, bool isDkm = false)
        {
            var uriDelDkm = new Uri(string.Format(UrlHelper.DELETE_URL + "?Id_Dkm={0}", id));
            var uriDelEvent = new Uri(string.Format(UrlHelper.DELETE_URL + "?Id_Event={0}", id));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage respone = null;
                    if (isDkm)
                        respone = await client.GetAsync(uriDelDkm);
                    else
                        respone = await client.GetAsync(uriDelEvent);
                    Debug.WriteLine(respone.ToString());

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

        /// <summary>
        /// Service untuk menyimpan data Event Masjid dengan method POST dan PUT
        /// </summary>
        /// <param name="dkm">data dkm yang akan disimpan</param>
        /// <param name="isNewDkm">true: buat baru; false: update</param>
        /// <returns></returns>
        public async Task<bool> SaveDkm(Dkm dkm, bool isNewDkm = false)
        {
            var uriCreate = new Uri(string.Format(UrlHelper.DKM_URL));
            var uriUpdate = new Uri(string.Format(UrlHelper.EDIT_URL));

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    var post = JsonConvert.SerializeObject(dkm);
                    var content = new StringContent(post, Encoding.UTF8, "application/json");
                    //HttpContent content = new StringContent(post);
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    HttpResponseMessage respone = null;
                    if (isNewDkm)
                        respone = await client.PostAsync(uriCreate, content);
                    else
                        respone = await client.PostAsync(uriUpdate, content);

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

    public static class MyDkm
    {
        public static string Id_Dkm { get; set; }

        public static string Uname_Dkm { get; set; }

        public static string Pass_Dkm { get; set; }

        public static string Alamat_Dkm { get; set; }

        public static string Tlp_Dkm { get; set; }

        public static string Email_Dkm { get; set; }

        public static string Ketua_Dkm { get; set; }

        public static string Masjid_Dkm { get; set; }
    }
}
