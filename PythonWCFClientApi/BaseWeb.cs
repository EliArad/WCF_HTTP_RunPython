using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
 

namespace BaseWebApi
{
    public class HarmoniServiceResponse
    {
        public bool status;
        public string msg;
    }
    public class BaseWeb
    {

        public class Token
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string userName { get; set; }
            [JsonProperty(".issued")]
            public string issued { get; set; }
            [JsonProperty(".expires")]
            public string expires { get; set; }
        }

        
        protected string m_baseUrl;
        protected Token m_token;
         

        public BaseWeb(string baseUrl, Token token)
        {
            m_token = token;
            m_baseUrl = baseUrl;
        }


        public BaseWeb(string baseUrl)
        {
            m_baseUrl = baseUrl;
        }

        public BaseWeb(string ipAddress, int port)
        {
            m_baseUrl = string.Format("http://{0}:{1}/", ipAddress, port);
        }

        public void SetToken(Token token)
        {
            m_token = token;
        }


        bool HandleResponse(HttpResponseMessage result, out string outMessage)
        {
            if (result.IsSuccessStatusCode)
            {
                string json = "";
                using (HttpContent content = result.Content)
                {
                    json = content.ReadAsStringAsync().Result;
                    json = JsonConvert.DeserializeObject<string>(json);
                }
                outMessage = "Status: " + result.ReasonPhrase + "\nMessage: " + json;
                return true;
            }
            else
            {
                using (HttpContent content = result.Content)
                {
                    outMessage = content.ReadAsStringAsync().Result;
                }
                return false;
            }
        }

        public bool PostAsync(string command, string ContentJson, out string outMessage)
        {
            outMessage = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {

                    var postTask = client.PostAsync($"{command}/{ContentJson.ToString()}", null);
                    postTask.Wait();
                    var result = postTask.Result;
                    return HandleResponse(result, out outMessage);
                }
                catch (Exception ex)
                {

                    outMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return false;
                }
            }
        }


        public async void GetAsync(string command, Action<bool, string> cb)
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {

                    var result = await client.GetAsync($"{command}");

                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            string response = JsonConvert.DeserializeObject<string>(json);
                            cb(true, response);
                        }
                    }
                    else
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            string response = JsonConvert.DeserializeObject<string>(json);
                            cb(false, response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string outMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    cb(false, outMessage);
                }
            }
        }

        public bool GetSync<T>(string command, out T t, out string outMessage)  where T : struct 
        {
            outMessage = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {

                    var response = client.GetAsync($"{command}");
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            t = JsonConvert.DeserializeObject<T>(json);
                            return true;
                        }
                    }
                    else
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            HarmoniServiceResponse r = JsonConvert.DeserializeObject<HarmoniServiceResponse>(json);
                            outMessage = r.msg;
                            t = new T();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    t = new T();
                    return false;
                }
            }
        }

        public bool GetSync(string command, out string t, out string outMessage) 
        {
            outMessage = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {

                    var response = client.GetAsync($"{command}");
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent content = result.Content)
                        {
                            t = content.ReadAsStringAsync().Result;
                            return true;
                        }
                    }
                    else
                    {
                        using (HttpContent content = result.Content)
                        {
                            outMessage = content.ReadAsStringAsync().Result;
                            t = string.Empty;
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    t = string.Empty;
                    return false;
                }
            }
        }

        public bool GetSync<T,W>(string command, T t, out W w, out string outMessage) where W : struct
        {
            outMessage = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {

                    var response = client.GetAsync($"{command}");
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            w = JsonConvert.DeserializeObject<W>(json);
                            return true;
                        }
                    }
                    else
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            HarmoniServiceResponse r = JsonConvert.DeserializeObject<HarmoniServiceResponse>(json);
                            outMessage = r.msg;
                            w = new W();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    w = new W();
                    return false;
                }
            }
        }

        public async void PostAsync<T>(string command, T t, Action<bool, HarmoniServiceResponse> cb)
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {
                    var result = await client.PostAsJsonAsync($"{command}", t);
                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            cb(true, JsonConvert.DeserializeObject<HarmoniServiceResponse>(json));
                        }
                    }
                    else
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            HarmoniServiceResponse r = new HarmoniServiceResponse();
                            r.msg = json;
                            cb(false, r);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HarmoniServiceResponse r = new HarmoniServiceResponse();
                    r.msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    cb(false, r);
                }
            }
        }

        public async void PostAsync<T,W>(string command, T t, Action<bool, string, W> cb) where W : struct
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {
                    var result = await client.PostAsJsonAsync($"{command}", t);
                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            W w = JsonConvert.DeserializeObject<W>(json);
                            cb(true, json,w);
                        }
                    }
                    else
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;                             
                            cb(false, json, new W());
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    cb(false,  msg, new W());
                }
            }
        }
        public bool PostSync<T>(string command, T t, out string outMessage)
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {
                    var response = client.PostAsJsonAsync($"{command}", t);
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                        }
                        outMessage = string.Empty;
                        return true;
                    }
                    else
                    {
                        using (HttpContent content = result.Content)
                        {
                            outMessage = content.ReadAsStringAsync().Result;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return false;
                }
            }
        }

        public bool PostSync<T,W>(string command, T t, out W w, out string outMessage) where W: struct
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {
                    var response = client.PostAsJsonAsync($"{command}", t);
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            w = JsonConvert.DeserializeObject<W>(json);
                        }
                        outMessage = string.Empty;
                        return true;
                    }
                    else
                    {
                        using (HttpContent content = result.Content)
                        {
                            outMessage = content.ReadAsStringAsync().Result;
                        }
                        w = new W(); 
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    w = new W();
                    return false;
                }
            }
        }
        public bool PostSync(string command, out string outMessage)
        {

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(m_baseUrl);
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (m_token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token.access_token);

                try
                {
                    var response = client.PostAsync($"{command}", null);
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string json = "";
                        using (HttpContent content = result.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                        }
                        outMessage = string.Empty;
                        return true;
                    }
                    else
                    {
                        using (HttpContent content = result.Content)
                        {
                            outMessage = content.ReadAsStringAsync().Result;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return false;
                }
            }
        }
    }
}
