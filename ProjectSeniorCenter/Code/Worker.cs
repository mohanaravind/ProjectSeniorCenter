using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectSeniorCenter.UI;
using System.Threading;
using ProjectSeniorCenter.Code.Utility;

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
            if (Configurations.User.Contains(Configurations.Volunteer))
            {
                //Start the sniffer process

            }
            else
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


        }

    }
}
