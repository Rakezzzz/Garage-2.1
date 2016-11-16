﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._1.Models.Exceptions
{
    public class SpotAllreadyOccupiedException: Exception
    {
        /// <summary>
        /// Just create the exception
        /// </summary>
        public SpotAllreadyOccupiedException()
        {

        }

        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public SpotAllreadyOccupiedException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="innerException">Exception inner cause</param>
        public SpotAllreadyOccupiedException(string message, Exception inner)
            : base(message, inner)
        {

        }

        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        protected SpotAllreadyOccupiedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}