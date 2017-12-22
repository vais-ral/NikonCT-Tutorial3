using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;


using IpcContractClientInterface;
using AppLog = IpcUtil.Logging;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Tutorial3_AcquiringAnImage
{
    public partial class UserForm : Form
    {
        /// <summary>Are we in design mode</summary>
        protected bool mDesignMode { get; private set; }

        #region Standard IPC Variables

        /// <summary>This ensures consistent read and write culture</summary>
        private NumberFormatInfo mNFI = new CultureInfo("en-GB", true).NumberFormat; // Force UN English culture

        /// <summary>Collection of all IPC channels, this object always exists.</summary>
        private Channels mChannels = new Channels();

        #endregion Standard IPC Variables

        #region Application Variables

        // Application connection status
        private Channels.EConnectionState mApplicationState;

        // Flag for Average Complete
        private Boolean mImageAverageComplete = false;

        // Flag for Image Save complete
        private Boolean mImageSaveComplete = false;

        // Number of images to average
        private int mNumberImagesToAverage = 1;

        // String constant for Directory
        const string mDirectory = @"C:\Users\User\Pictures";

        // String for filename
        private string mFilename = "untitled";

        // Thread for image average save routine
        private Thread mThreadImageAverageSave = null;

        #endregion Application Variables

        public UserForm()
        {
            try
            {
                mDesignMode = (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Designtime);
                InitializeComponent();
                if (!mDesignMode)
                {
                    // Tell normal logging who the parent window is.
                    AppLog.SetParentWindow = this;
                    AppLog.TraceInfo = true;
                    AppLog.TraceDebug = true;

                    mChannels = new Channels();
                    // Enable the channels that will be controlled by this application.
                    // For the generic IPC client this is all of them!
                    // This just sets flags, it does not actually open the channels.
                    mChannels.AccessApplication = true;
                    mChannels.AccessXray = false;
                    mChannels.AccessManipulator = false;
                    mChannels.AccessImageProcessing = true;
                    mChannels.AccessInspection = false;
                    mChannels.AccessInspection2D = false;
                    mChannels.AccessCT3DScan = false;
                    mChannels.AccessCT2DScan = false;
                }
            }
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        #region Channel connections

        /// <summary>Attach to channel and connect any event handlers</summary>
        /// <returns>Connection status</returns>
        private Channels.EConnectionState ChannelsAttach()
        {
            try
            {
                if (mChannels != null)
                {
                    Channels.EConnectionState State = mChannels.Connect();
                    if (State == Channels.EConnectionState.Connected)  // Open channels
                    {
                        // Attach event handlers (as required)

                        if (mChannels.Application != null)
                        {
                            mChannels.Application.mEventSubscriptionHeartbeat.Event +=
                                new EventHandler<CommunicationsChannel_Application.EventArgsHeartbeat>(EventHandlerHeartbeatApp);
                        }


                        if (mChannels.ImageProcessing != null)
                        {
                            mChannels.ImageProcessing.mEventSubscriptionHeartbeat.Event +=
                                new EventHandler<CommunicationsChannel_ImageProcessing.EventArgsHeartbeat>(EventHandlerHeartbeatIP);
                            mChannels.ImageProcessing.mEventSubscriptionImageProcessing.Event +=
                                new EventHandler<CommunicationsChannel_ImageProcessing.EventArgsIPEvent>(EventHandlerIPEvent);
                        }


                    }
                    return State;
                }
            }
            catch (Exception ex) { AppLog.LogException(ex); }
            return Channels.EConnectionState.Error;
        }

        /// <summary>Detach channel and disconnect any event handlers</summary>
        /// <returns>true if OK</returns>
        private bool ChannelsDetach()
        {
            try
            {
                if (mChannels != null)
                {
                    // Detach event handlers

                    if (mChannels.Application != null)
                    {
                        mChannels.Application.mEventSubscriptionHeartbeat.Event -=
                            new EventHandler<CommunicationsChannel_Application.EventArgsHeartbeat>(EventHandlerHeartbeatApp);
                    }

                    if (mChannels.ImageProcessing != null)
                    {
                        mChannels.ImageProcessing.mEventSubscriptionHeartbeat.Event -=
                            new EventHandler<CommunicationsChannel_ImageProcessing.EventArgsHeartbeat>(EventHandlerHeartbeatIP);
                        mChannels.ImageProcessing.mEventSubscriptionImageProcessing.Event -=
                            new EventHandler<CommunicationsChannel_ImageProcessing.EventArgsIPEvent>(EventHandlerIPEvent);
                    }


                    Thread.Sleep(100); // A breather for events to finish!
                    return mChannels.Disconnect(); // Close channels
                }
            }
            catch (Exception ex) { AppLog.LogException(ex); }
            return false;
        }

        #endregion Channel connections

        #region Heartbeat from host

        void EventHandlerHeartbeatApp(object aSender, CommunicationsChannel_Application.EventArgsHeartbeat e)
        {
            try
            {
                if (mChannels == null || mChannels.Application == null)
                    return;
                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate { EventHandlerHeartbeatApp(aSender, e); });
                else
                {
                    //your code goes here....
                }
            }
            catch (ObjectDisposedException) { } // ignore
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        void EventHandlerHeartbeatIP(object aSender, CommunicationsChannel_ImageProcessing.EventArgsHeartbeat e)
        {
            try
            {
                if (mChannels == null || mChannels.ImageProcessing == null)
                    return;
                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate { EventHandlerHeartbeatIP(aSender, e); });
                else
                {
                    //your code goes here...
                }
            }
            catch (ObjectDisposedException) { } // ignore
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        #endregion Heartbeat from host

        #region STATUS FROM HOST

        #region ImageProcessing

        void EventHandlerIPEvent(object aSender, CommunicationsChannel_ImageProcessing.EventArgsIPEvent e)
        {
            try
            {
                if (mChannels == null || mChannels.ImageProcessing == null)
                    return;
                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate { EventHandlerIPEvent(aSender, e); }); // Make it non blocking if called form this UI thread
                else
                {

                    Debug.Print(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss.fff") + " : e.IPEvent.EventType=" + e.IPEvent.EventType.ToString());

                    switch (e.IPEvent.EventType)
                    {
                        case IpcContract.ImageProcessing.IPEvent.EEventType.Live:
                            // Your code goes here...
                            break;
                        case IpcContract.ImageProcessing.IPEvent.EEventType.Capture:
                            // Your code goes here...
                            break;
                        case IpcContract.ImageProcessing.IPEvent.EEventType.Average:
                            // Your code goes here...
                            break;
                        case IpcContract.ImageProcessing.IPEvent.EEventType.AverageComplete:
                            // flag set to true when image averaging complete
                            mImageAverageComplete = true;
                            break;
                        case IpcContract.ImageProcessing.IPEvent.EEventType.LoadImageComplete:
                            // Your code goes here...
                            break;
                        case IpcContract.ImageProcessing.IPEvent.EEventType.SaveImageComplete:
                            // flag set to true when image saved
                            mImageSaveComplete = true;
                            break;
                        default:
                            // Your code goes here...
                            break;
                    }
                }
            }
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        #endregion

        #endregion Status from host

        #region User functions

        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Attach channels
                mApplicationState = ChannelsAttach();

                if (mApplicationState == Channels.EConnectionState.Connected)
                    Debug.Print(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss.fff") + " : Connected to Inspect-X");
                else
                    Debug.Print(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss.fff") + " : Problem in connecting to Inspect-X");
            }
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Detach channels
                ChannelsDetach();

                Debug.Print(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss.fff") + " : Disconnected from Inspect-X");
            }
            catch (Exception ex) { AppLog.LogException(ex); }
        }

        private void numericUpDown_NumberImagesToAverage_ValueChanged(object sender, EventArgs e)
        {
            mNumberImagesToAverage = (int) numericUpDown_NumberImagesToAverage.Value;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mFilename = textBox_Filename.Text.ToString();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Initialise the thread with the ImageAverageSave routine
            mThreadImageAverageSave = new Thread(ImageAverageSave);
            // Start the thread
            mThreadImageAverageSave.Start();
        }

        #endregion User functions

        #region Image Processing Routines

        private void ImageAverageSave()
        {

            // If ApplicationState is not connected then immediately exit the routine
            if (mApplicationState != Channels.EConnectionState.Connected)
                return;

            // For safety, disable the Start button
            this.Invoke((MethodInvoker)delegate { btn_Start.Enabled = false; });

            // Set filepath
            string aFilepath = mDirectory + @"\" + mFilename + @".tif";

            // Set flags to false
            mImageAverageComplete = false;
            mImageSaveComplete = false;

            // Average set number of images
            mChannels.ImageProcessing.Image.Average(mNumberImagesToAverage, false);

            // Wait until average is complete
            while (!mImageAverageComplete)
                Thread.Sleep(10);

            // Save image
            mChannels.ImageProcessing.Image.SaveAsTiff(aFilepath, false, false, false);

            // Wait until save has completed
            while (!mImageSaveComplete)
                Thread.Sleep(10);

            // Re-enable the Start button
            this.Invoke((MethodInvoker)delegate { btn_Start.Enabled = true; });

        }

        #endregion Image Processing Routines

    }
}