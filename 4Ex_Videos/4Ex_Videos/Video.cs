using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace _4Ex_Videos
{
    class Video
    {
        private string url, titol;
        private string username_Usuari;
        private List<string> tags = new List<string>();
        public string URL
        {
            get => this.url;
            set => this.url = value;
        }
        public string TITOL
        {
            get => this.titol;
            set => this.titol = value;
        }

        public string USERNAME_USUARI
        {
            get => this.username_Usuari;
            set => this.username_Usuari = value;
        }
        public List<string> TAGS
        {
            get => this.tags;
        }
        public void addTags(string tag)
        {
            this.tags.Add(tag);
        }
    }
}
