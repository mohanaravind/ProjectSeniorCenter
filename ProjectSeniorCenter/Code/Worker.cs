using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectSeniorCenter.UI;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;
using ProjectSeniorCenter.Code.Entity;

namespace ProjectSeniorCenter.Code
{
    class Worker
    {

        /// <summary>
        /// Starts the main execution
        /// </summary>
        public void Work()
        {
            //Get the type of user
            string userName = Configurations.User;

            //If its a volunteer
            if (IsVolunteer())
                StartSniffer();
            else
                StartNotifier();
        }

        /// <summary>
        /// Gets the sniffer configuration data from the database
        /// </summary>
        private void GetSnifferConfigData()
        {
            
        }

        /// <summary>
        /// Starts the notifier
        /// </summary>
        private void StartNotifier()
        {
            //Create the volunteer form
            Volunteer volunteer = new Volunteer();

            //Start the notifier process
            Notifier notifier = new Notifier(volunteer);

            //Create notifier thread
            Thread thNotifier = new Thread(new ThreadStart(notifier.Start));

            //Set the notifier thread
            volunteer.NotifierThread = thNotifier;

            //Start the notification thread
            thNotifier.Start();
        }

        /// <summary>
        /// Starts the sniffer
        /// </summary>
        private void StartSniffer()
        {
            //Start the sniffer process
            Sniffer sniffer = Sniffer.CreateSniffer();

            //Create the sniffer thread
            Thread thSniffer = new Thread(new ThreadStart(sniffer.Start));

            //Set the sniffer status
            sniffer.IsAlive = true;

            //Start sniffing
            thSniffer.Start();

            //While the sniffer thread is running
            while (sniffer.IsAlive)
            {
                Thread.Sleep(Configurations.SnifferPollTime);                
            }

            //Shuts the sniffer down
            ShutDownSniffer(thSniffer);
        }

        /// <summary>
        /// Shuts the sniffer down
        /// </summary>
        /// <param name="thSniffer"></param>
        private void ShutDownSniffer(Thread thSniffer)
        {
            //Abort the sniffer
            thSniffer.Abort();
            thSniffer.Join();
        }

        /// <summary>
        /// Checks whether the logged in user is a volunteer
        /// </summary>
        /// <returns></returns>
        public Boolean IsVolunteer()
        {
            //Declarations
            Boolean result = false;

            try
            {
                //Get the name of the volunteer who has logged in
                String volunteer = Configurations.User;

                //Check whether this volunteer has PII information 
                result = SnifferConfigHandler.GetSnifferConfigHandler().Person.Volunteer.Equals(volunteer);

                //TODO://If its false either the user should not be a volunteer or a volunteer who's PII are not available
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

    }
}
