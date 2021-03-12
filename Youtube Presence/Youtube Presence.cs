using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Automation;
using System.Threading;
using DiscordRpcDemo;
using VideoLibrary;

namespace Youtube_Presence
{
    public partial class YoutubePresence : Form
    {
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;

        public YoutubePresence()
        {
            InitializeComponent();

            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("705480188624109660", ref this.handlers, true, null);
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("705480188624109660", ref this.handlers, true, null);
            this.presence.details = "No Video Found";
            this.presence.state = "Youtube Presence By LeonimusT";
            this.presence.largeImageKey = "large";
            //this.presence.smallImageKey = "small";
            //this.presence.largeImageText = "Image 1 Text";
            //this.presence.smallImageText = "Image 2 Text";
            DiscordRpc.UpdatePresence(ref this.presence);

            Chrome();

        }


        public void Chrome()
        {
                
                
                Process[] procsChrome = Process.GetProcessesByName("chrome");

                if(procsChrome.Length > 1)
                {
                    Console.WriteLine("Chrome Process found");
                    while (true)
                    {
                    Thread.Sleep(25000);
                        foreach (Process chrome in procsChrome)
                        {
                            if (chrome.MainWindowHandle == IntPtr.Zero)
                            {
                                continue;
                            }
                            AutomationElement soruceElement = AutomationElement.FromHandle(chrome.MainWindowHandle);

                            Condition condition = new OrCondition(new Condition[2]
                            {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, (object) ControlType.Edit),
                    new PropertyCondition(AutomationElement.AccessKeyProperty, (object) "Ctrl+L", PropertyConditionFlags.IgnoreCase),
                            });

                            AutomationElement elmUrlBar = soruceElement.FindFirst(TreeScope.Descendants, condition);

                            if (elmUrlBar != null)
                            {
                                AutomationPattern[] pattern = elmUrlBar.GetSupportedPatterns();
                                if (pattern.Length > 0)
                                {
                                    ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(pattern[0]);
                                    if (val.Current.Value.Contains("youtube.com/watch?v="))
                                    {
                                        Console.WriteLine("youtube url found " + val.Current.Value);
                                        YouTube ytb = YouTube.Default;
                                        var vid = ytb.GetVideo(val.Current.Value);

                                        string ttl = vid.Title;

                                        this.presence.details = ttl;
                                        this.presence.state = "Youtube Presence By LeonimusT";
                                        this.presence.largeImageKey = "large";
                                        //this.presence.smallImageKey = "small";
                                        //this.presence.largeImageText = "Image 1 Text";
                                        //this.presence.smallImageText = "Image 2 Text";
                                        DiscordRpc.UpdatePresence(ref this.presence);
                                    }
                                    else
                                    {
                                        Console.WriteLine("youtube url not found. Current url " + val.Current.Value);
                                        this.presence.details = "No Video Found";
                                        this.presence.state = "Youtube Presence By LeonimusT";
                                        this.presence.largeImageKey = "large";
                                        //this.presence.smallImageKey = "small";
                                        //this.presence.largeImageText = "Image 1 Text";
                                        //this.presence.smallImageText = "Image 2 Text";
                                        DiscordRpc.UpdatePresence(ref this.presence);
                                    }

                                }
                            }
                        }
                    }
                    
                }else
                {
                // Console.WriteLine("Chrome not found");
                Process[] procsfirefox = Process.GetProcessesByName("firefox");
                if (procsfirefox.Length > 1)
                    {
                        Console.WriteLine("Firefox Process found");
                        Console.WriteLine("To continue you will have to write below what and write in the search bar. Go see the screen shot if you did not understand ->> https://imgur.com/a/J0EV3uv");
                        string nameProperty = Console.ReadLine();
                    while (true)
                    {
                        Thread.Sleep(25000);

                        foreach (Process firefox in procsfirefox)
                        {
                            if (firefox.MainWindowHandle == IntPtr.Zero)
                            {
                                continue;
                            }
                            AutomationElement sourceElement = AutomationElement.FromHandle(firefox.MainWindowHandle);
                            //works with latest version of firefox and for older version replace 'Search with Google or enter address' with this 'Search or enter address'

                            AutomationElement editBox = sourceElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, nameProperty));
                            if (editBox != null)
                            {
                                ValuePattern val = ((ValuePattern)editBox.GetCurrentPattern(ValuePattern.Pattern));
                                // Console.WriteLine("\n Firefox URL found: " + val.Current.Value);
                                if (val.Current.Value.Contains("https://www.youtube.com/watch?v="))
                                {
                                    Console.WriteLine("youtube url found " + val.Current.Value);
                                    YouTube ytb = YouTube.Default;
                                    var vid = ytb.GetVideo(val.Current.Value);

                                    string ttl = vid.Title;

                                    this.presence.details = ttl;
                                    this.presence.state = "Youtube Presence By LeonimusT";
                                    this.presence.largeImageKey = "large";
                                    //this.presence.smallImageKey = "small";
                                    //this.presence.largeImageText = "Image 1 Text";
                                    //this.presence.smallImageText = "Image 2 Text";
                                    DiscordRpc.UpdatePresence(ref this.presence);
                                }
                                else
                                {
                                    Console.WriteLine("youtube url not found. Current url " + val.Current.Value);
                                    this.presence.details = "No Video Found";
                                    this.presence.state = "Youtube Presence By LeonimusT";
                                    this.presence.largeImageKey = "large";
                                    //this.presence.smallImageKey = "small";
                                    //this.presence.largeImageText = "Image 1 Text";
                                    //this.presence.smallImageText = "Image 2 Text";
                                    DiscordRpc.UpdatePresence(ref this.presence);
                                }
                            }else
                            {
                                Console.WriteLine("You didn't spell out what's in the search bar. Review the screen shot to better understand - >> https://imgur.com/a/J0EV3uv");
                                Console.WriteLine("Please rewrite what is in the search bar");
                                nameProperty = Console.ReadLine();
                            }
                        }
                    }
                        
                }else
                    {
                        Console.WriteLine("Firefox or Chrome process not found");
                    }
                }
            
        }
    }
}
