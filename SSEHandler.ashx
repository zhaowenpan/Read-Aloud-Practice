<%@ WebHandler Language="C#" Class="SSEHandler" %>

using System;
using System.Web;
////////////////////////////////////////////////////////////////////////////////////////////////////////
public class SSEHandler : IHttpHandler {
    ////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public void ProcessRequest(HttpContext context)
    {
        HttpResponse Response = context.Response;
        DateTime startDate = DateTime.Now;
        Response.ContentType = "text/event-stream";
        while (startDate.AddMinutes(10) > DateTime.Now)
        {
            if (Class1.j != Class1.CharacterPositionStart_int)
            {
                Class1.j = Class1.CharacterPositionStart_int;
                Response.Write("event: cursorData\n");
                Response.Write(string.Format("data: {0},{1},{2}\n\n", Class1.j, Class1.characterCount_int, Class1.AudioPosition_TimeSpan));
                Response.Flush();
            }

            if(Class1.OnSpeakCompleted__Boolean == true)
            {
                Class1.OnSpeakCompleted__Boolean = false;
                //•Set the "Content-Type" header to "text/event-stream"

                Response.Write("event: OnSpeakCompleted\n");
                Response.Write(string.Format("data: {0}\n\n", Class1.OnSpeakCompleted__Boolean));
                Response.Flush();

                String s1 = "abc";
                String streamAudio_Base64String;
                byte[] byte_array = Class1.streamAudio.ToArray();
                //byte_array = System.Text.Encoding.UTF8.GetBytes(s1);
                streamAudio_Base64String = Convert.ToBase64String(byte_array);

                // byte[] encodedDataAsBytes = System.Convert.FromBase64String(streamAudio_base64String);
                //string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted byte_array length is : " + byte_array.Length.ToString() + "\n\n");
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted byte_array length is : " + byte_array.Length.ToString() + "\n\n");
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted streamAudio_Base64String is : " + streamAudio_Base64String + "\n\n");
                //byte_array = Class1.streamAudio.ToArray();
                Response.Write("event: TextToSpeechBase64StringData\n");
                //Response.Write(string.Format("data: {0}\n\n", Class1.OnSpeakCompleted__Boolean));
                //Response.Write(string.Format("data: {0}\n\n", ));

                //Response.BinaryWrite(Class1.streamAudio.ToArray());
                //    Response.Flush();
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted 1 : " + "\n\n");
                //var s = "data: ";
                //Response.Write(s);
                //Response.BinaryWrite(Class1.streamAudio.ToArray());
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted 2 : " + "\n\n");
                Response.Write("data:" + streamAudio_Base64String);
                Response.Write("\n\n");
                //Response.Write(string.Format("data: {0}\n\n", streamAudio_Base64String));
                Response.Flush();
                System.Diagnostics.Debug.Write("ashx OnSpeakCompleted 3 : " + "\n\n");
                //Response.Write(string.Format("data: {0}\n\n", Class1.streamAudio.ToArray()));
                //Response.Flush();
                ////Response.Close();
                ////Response.End();
                Class1.streamAudio.SetLength(0);

            }

        }
        Response.Close();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsReusable {
        get {
            return false;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
}
////////////////////////////////////////////////////////////////////////////////////////////////////////