using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQueueMessagesFunction

{
	internal class PatientUQMessage
    {
		public string patientID
        {
			get; set; 
        }

		public bool hasInsurance
			{ get; set; }

		public string policyNumber { get; set; }
		public string provider { get; set; }
    }
}

