using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cynfo1._0.App_Start
{
    public class FirebaseInit
    {

        public static IFirebaseConfig Firebaseconfig { get; }

        public static IFirebaseClient Firebaseclient { get; }

        static FirebaseInit()
        {
            Firebaseconfig = new FirebaseConfig
            {
                AuthSecret = "KGJV1leYk8S8cy9L8ViSvMGiyeWmLYrnoV4kpBRn",
                BasePath = "https://cynfoapp.firebaseio.com/"
            };

            Firebaseclient = new FirebaseClient(Firebaseconfig);
        }




    }
}