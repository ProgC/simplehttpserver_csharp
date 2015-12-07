using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace simplehttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[1];
            args[0] = "http://localhost:8080/myserver/";

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Not support httplistener");
                return;
            }

            HttpListener listener = new HttpListener();

            foreach (string s in args)
            {
                listener.Prefixes.Add(s);
            }

            listener.Start();

            Console.WriteLine("Listening");

            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // construct a response.
            string msg = "<HTML><BODY>Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg);

            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            listener.Stop();
        }
    }
}
