using System;
using System.Collections.Generic;
using System.IO;

using net.openstack.Providers.Rackspace;
using net.openstack.Providers.Rackspace.Objects;
using net.openstack.Core.Providers;
using net.openstack.Core.Exceptions.Response;
using net.openstack.Core.Domain;


namespace CloudServersSnapshot
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Usage: {0} username api_key [region (US|UK)]", Environment.CommandLine);
                Environment.Exit(1);
            }
            RackspaceImpersonationIdentity auth = new RackspaceImpersonationIdentity();
            auth.Username = args[0];
            auth.APIKey = args[1];

            if (args.Length == 3)
            {
                if (args[2] != "UK" && args[2] != "US")
                {
                    Console.WriteLine("region must be either US or UK", Environment.CommandLine);
                    Environment.Exit(1);
                }
                switch (args[2])
                {
                    case "UK": { auth.CloudInstance = CloudInstance.UK; }; break;
                    case "US": { auth.CloudInstance = CloudInstance.Default; }; break;
                }
            }
            else
            {
                auth.CloudInstance = CloudInstance.Default;
            }

            try
            {
                IIdentityProvider identityProvider = new CloudIdentityProvider();
                var userAccess = identityProvider.Authenticate(auth);
            }
            catch (ResponseException ex2)
            {
                Console.WriteLine("Authentication failed with the following message: {0}", ex2.Message);
                Environment.Exit(1);
            }

            var cloudServers = new CloudServersProvider(auth);
            var servers = cloudServers.ListServers();
            foreach (Server serv in servers)
            {
                var date = System.DateTime.Now;
                var success = serv.CreateSnapshot(serv.Name + "_" + date.Year + "-" + date.Month + "-" + date.Day);
                if (success)
                {
                    Console.WriteLine("Image for server {0} has been created successfully.", serv.Name);
                }
                else
                {
                    Console.WriteLine("Image for server {0} could not be created.", serv.Name);
                }
            }
        }
    }
}
