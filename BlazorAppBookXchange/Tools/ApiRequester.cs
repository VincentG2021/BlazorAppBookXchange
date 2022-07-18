﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Tools
{
    public class ApiRequester
    {
        private string _baseAddress;

        private HttpClient _client;

        public HttpClient Client
        {
            get { return _client; }
        }

        public ApiRequester(string baseAdress)
        {
            _client = new HttpClient();
            _baseAddress = baseAdress;
            _client.BaseAddress = new Uri(_baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<TResult> Get<TResult>(string url, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = await _client.GetAsync(url))
            {
                if (!message.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }

                string json = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(json);
            }
        }

        public async Task<TResult> Post<TEntity, TResult>(string url, TEntity entity, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage responseMessage = await _client.PostAsync(url, content))
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    //throw new HttpRequestException();
                    return default(TResult);
                }

                return await responseMessage.Content.ReadFromJsonAsync<TResult>();
            }
        }

        public async Task<TResult> Put<TEntity, TResult>(string url, TEntity entity, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage responseMessage = await _client.PutAsync(url, content))
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    //throw new HttpRequestException();
                    return default(TResult);
                }

                return await responseMessage.Content.ReadFromJsonAsync<TResult>();
            }
        }

        public async Task<TResult> Delete<TResult>(string url, int id, string token = null)
        {
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = await _client.DeleteAsync(url + id))
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();

                return await message.Content.ReadFromJsonAsync<TResult>();
            }
        }

        public async void Update<TEntity>(string url, int id, TEntity entity, string token = null)
        {

            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = await _client.PutAsync(url + id, content))
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }
    }
}
