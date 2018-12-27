using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
//[System.Web.Services.WebMethod]

public partial class _Default : System.Web.UI.Page
{
    /////////////////////////////////////////////////////////////////////////////
    static System.Speech.Synthesis.SpeechSynthesizer MS_SpeechSynthesizer;
    //= new System.Speech.Synthesis.SpeechSynthesizer();
    

    static System.Media.SoundPlayer m_SoundPlayer;
    static String MS_VoiceName_String = "";
    static String ReadAloudRate_String;
    static Int32 ReadAloudRate_Int32;
    static int i = 0;
    static int jump = 0;
    //GlobalClass globalClass;
    //static SSE_Handler sSE_Handler;
    /////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        jump = 0;
        if (!Page.IsPostBack)
        {
            Class1.CharacterPositionStart_int = 0;
            Class1.j = 0;
            Class1.characterCount_int = 0;
            Myinitialize();
        }
    }
    /////////////////////////////////////////////////////////////////////////////
    private void Myinitialize()
    {
        MS_SpeechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
        Thread t = new Thread(() =>
        {
            MS_SpeechSynthesizer.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(TextBox_ReadAloudText_OnSpeakStarted);
            MS_SpeechSynthesizer.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(TextBox_ReadAloudText_OnSpeakProgress);
            MS_SpeechSynthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(TextBox_ReadAloudText_OnSpeakCompleted);
            MS_SpeechSynthesizer.BookmarkReached += new EventHandler<BookmarkReachedEventArgs>(TextBox_ReadAloudText__BookmarkReached);
        });
        t.Start();
        t.Join();
        Add_DropDownList_ReadAloudRate();
        Add_DropDownList_MS_VoiceNameOnServer();

    }
    /////////////////////////////////////////////////////////////////////////////////
    static void TextBox_ReadAloudText__BookmarkReached(object sender, BookmarkReachedEventArgs e)
    {

    }
    /////////////////////////////////////////////////////////////////////////////////
    protected void TextBox_ReadAloudText_OnSpeakCompleted(object sender, SpeakCompletedEventArgs e)
    {
        Class1.OnSpeakCompleted__Boolean = true;
    }
    /////////////////////////////////////////////////////////////////////////////
    protected void TextBox_ReadAloudText_OnSpeakProgress(object sender, SpeakProgressEventArgs e)
    {
        Class1.CharacterPositionStart_int = e.CharacterPosition;
        Class1.characterCount_int = e.CharacterCount;
        Class1.AudioPosition_TimeSpan = e.AudioPosition;
        try
        {
            //System.Diagnostics.Debug.Write("e.CharacterPosition: " + e.CharacterPosition.ToString() + "\n\n");
            //System.Diagnostics.Debug.Write("e.AudioPosition: " + e.AudioPosition.ToString() + "\n\n");
            System.Diagnostics.Debug.Write("e.AudioPosition: " + e.AudioPosition.ToString() + "\n\n");
            System.Diagnostics.Debug.Write("----------------------------------------" + "\n\n");
        }
        catch (Exception ee)
        {
            System.Diagnostics.Debug.Write("OnSpeakProgress ee: " + ee.ToString() + "\n\n");
        }
        //        Console.WriteLine("Capacity = {0}, Length = {1}, Position = {2}\n",
        //memStream.Capacity.ToString(),
        //memStream.Length.ToString(),
        //memStream.Position.ToString());
        //
        //streamAudio.SetLength(0);



        //streamAudio.Position = 0;
        //m_SoundPlayer.Stream = null;
        //m_SoundPlayer.Stream = streamAudio;

        //    m_SoundPlayer.Play();
        //}
        //catch (Exception ee)
        //{
        //    System.Diagnostics.Debug.Write("my OnSpeakProgress ee: " + ee.ToString() + "\n\n");
        //}



        //sound.Position = 0;     // Manually rewind stream  
        //player.Stream = null;    // Then we have to set stream to null  
        //player.Stream = sound;  // And set it again, to force it to be loaded again...  
        //player.Play();




        ////streamAudio.Position = 0;
        //streamAudio.SetLength(0);
        //System.Diagnostics.Debug.Write("streamAudio.Length(0): " + streamAudio.Length.ToString() + "\n\n");
        //System.Diagnostics.Debug.Write(Class1.CharacterPositionStart_int.ToString()+"\n\n");


    }

    /////////////////////////////////////////////////////////////////////////////
    protected void TextBox_ReadAloudText_OnSpeakStarted(object sender, SpeakStartedEventArgs e)
    {
        //Class1.OnSpeakCompleted__String = "no";
        System.Diagnostics.Debug.Write("OnSpeakStarted" + "\n\n");
    }
    /////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////
    [System.Web.Services.WebMethod]
    public static string ReadAloudOnServer(String text_String)
    {
        Class1.OnSpeakCompleted__Boolean = false;
        Class1.streamAudio = new System.IO.MemoryStream();
        m_SoundPlayer = new System.Media.SoundPlayer();
        int textLength_Int;
        //text_String = TextBox_ReadAloudText.Text.Trim();
        //text_String = "Text box Read Aloud. Text box Read Aloud. Text box Read Aloud.";
        textLength_Int = text_String.Length;
        Thread Speak_Thread = new Thread(() =>
        {
            MS_SpeechSynthesizer.Rate = ReadAloudRate_Int32; //-10 to +10; 
            MS_SpeechSynthesizer.SetOutputToWaveStream(Class1.streamAudio);
            

            //MS_SpeechSynthesizer.SetOutputToAudioStream(streamAudio);
            //MS_SpeechSynthesizer.Speak(text_String);
            MS_SpeechSynthesizer.SpeakAsync(text_String);

            //streamAudio.Position = 0;
            //m_SoundPlayer.Stream = streamAudio;
            //try
            //{
            //    m_SoundPlayer.Play();
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.Write("my ReadAloudOnServer ee: " + e.ToString() + "\n\n");
            //}


        });
        Speak_Thread.Start();
        Speak_Thread.Join();
        //
        //System.Diagnostics.Debug.Write("Server 2 ReadAloud\n\n");

        string s;
        s = "ini";
        int i = 100;
        int j = 200;
        DateTime dateTime;
        dateTime = DateTime.Now;
        //try
        //{
        //    //System.Diagnostics.Debug.Write("Server IN ReadAloud");
        //}
        //catch (Exception e)
        //{
        //    s = e.ToString();
        //}
        int k;
        k = i + j;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用这种方法合适点')<script>");
        return dateTime.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////
    public string ReadAloud1()

    {
        //Thread Speak_Thread = new Thread(() =>
        //{
        //System.Speech.Synthesis.SpeechSynthesizer MS_SpeechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
        //MS_SpeechSynthesizer.SpeakAsync("Hwllo world");
        //});
        //Speak_Thread.Start();
        //Speak_Thread.Join();
        return ReadAloudRate_Int32.ToString();

    }
    /////////////////////////////////////////////////////////////////////////////
    [System.Web.Services.WebMethod]
    public static string GetCurrentTime(string name)
    {
        ////ReadAloud("",1);
        //Thread Speak_Thread = new Thread(() =>
        //{
        //    MS_SpeechSynthesizer.Rate = ReadAloudRate_Int32; //-10 to +10; 
        //    MS_SpeechSynthesizer.SpeakAsync(text_String);
        //});
        //Speak_Thread.Start();
        //Speak_Thread.Join();
        //try
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "addEventListener_SSEHandler(" + textLength_Int + ");", true);
        //    TextBox_ReadAloudText.Focus();
        //}
        //catch (Exception e)
        //{

        //}
        return "";
    }
    /////////////////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////////////////
    //public async Task DoSomethingAsync()
    //{
    //    await Task.Run(() =>
    //    {
    //        SpeechSynthesizer synth;
    //        synth = new SpeechSynthesizer();
    //        synth.Volume = 100;
    //        //synth.SetOutputToWaveStream(stream);
    //        synth.SpeakAsync(TextBox_ReadAloudText.Text);
    //        //System.Threading.Thread.Sleep(5000);
    //    });
    //    // return 1;
    //}


    /////////////////////////////////////////////////////////////////////////////////////
    protected void TextBoxReadAloudText_TextChanged(object sender, EventArgs e)
    {
    }
    /////////////////////////////////////////////////////////////////////////////////////

    protected void DropDownList_MS_VoiceNameOnServer_SelectedIndexChanged(object sender, EventArgs e)
    {
        MS_VoiceName_String = DropDownList_MS_VoiceNameOnServer.SelectedItem.ToString();
        TextBox_ReadAloudText.Text = MS_VoiceName_String;
        MS_SpeechSynthesizer.SelectVoice(MS_VoiceName_String);
    }
    /////////////////////////////////////////////////////////////////////////////
    protected void DropDownList_ReadAloudRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReadAloudRate_String = DropDownList_ReadAloudRate.SelectedItem.ToString();
        ReadAloudRate_Int32 = Int32.Parse(ReadAloudRate_String);
        MS_SpeechSynthesizer.Rate = ReadAloudRate_Int32;
        Class1.rate = ReadAloudRate_Int32;

    }
    /////////////////////////////////////////////////////////////////////////////
    private void Add_DropDownList_ReadAloudRate()
    {
        for (int i = 5; i > 0; i--)
        {
            DropDownList_ReadAloudRate.Items.Add(i.ToString());
        }
        DropDownList_ReadAloudRate.Items.Add("0");
        for (int i = -1; i >= -10; i--)
        {
            DropDownList_ReadAloudRate.Items.Add(i.ToString());
        }
        DropDownList_ReadAloudRate.Text = "0";
        ReadAloudRate_Int32 = Int32.Parse(DropDownList_ReadAloudRate.Text);
        Thread t = new Thread(() =>
        {
            MS_SpeechSynthesizer.Rate = ReadAloudRate_Int32;
        });
        t.Start();
        t.Join();
    }
    /////////////////////////////////////////////////////////////////////////////
    private void Add_DropDownList_MS_VoiceNameOnServer()
    {
        Thread t = new Thread(() =>
        {
            foreach (System.Speech.Synthesis.InstalledVoice MyVoice in MS_SpeechSynthesizer.GetInstalledVoices())
            {
                DropDownList_MS_VoiceNameOnServer.Items.Add(MyVoice.VoiceInfo.Name.ToString());
                MS_VoiceName_String = DropDownList_MS_VoiceNameOnServer.Text;
            }

        });

        t.Start();
        t.Join();
        //Microsoft David Desktop
        //Microsoft Zira Desktop
        //Microsoft Huihui Desktop
    }
    /////////////////////////////////////////////////////////////////////////////

}



