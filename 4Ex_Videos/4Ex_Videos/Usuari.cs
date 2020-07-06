using System;
using System.Collections.Generic;
using System.Text;

namespace _4Ex_Videos
{
    class Usuari
    {
        private string userName, nom, cognom, password;
        private DateTime Data;
        public string USERNAME
        {
            get => this.userName;
            set => this.userName = value;
        }
        public string NOM
        {
            get => this.nom;
            set => this.nom = value;
        }
        public string COGNOM
        {
            get => this.cognom;
            set => this.cognom = value;
        }
        public string PASSSWORD
        {
            get => this.password;
            set => this.password = value;
        }
        public DateTime DATA
        {
            get => this.Data;
            set => this.Data = value;
        }
    }
}
