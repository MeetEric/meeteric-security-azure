using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using System.Security;

namespace MeetEric.Security
{
    /// <summary>
    /// A helper used to obtain a bearer token from Azure Active Directory
    /// </summary>
    /// <remarks>
    /// Original code from: https://github.com/Azure-Samples/app-service-api-dotnet-contact-list/blob/master/ContactsList.API/ServicePrincipal.cs
    /// </remarks>
    public static class ServicePrincipal
    {
        private static readonly string _authority;
        private static readonly string _resource;
        private static readonly string _clientId;
        private static readonly SecureString _clientSecret;

        static ServicePrincipal()
        {
            _authority = CloudConfigurationManager.GetSetting("ida:Authority");
            _resource = CloudConfigurationManager.GetSetting("ida:Resource");
            _clientId = CloudConfigurationManager.GetSetting("ida:ClientId");
            _clientSecret = CloudConfigurationManager.GetSetting("ida:ClientSecret").AsSecureString();
        }

        public static AuthenticationResult GetS2SAccessTokenForAzureAd()
        {
            return GetS2SAccessToken(_authority, _resource, _clientId, _clientSecret);
        }

        ///<summary>
        /// Gets an application token used for service-to-service (S2S) API calls.
        ///</summary>
        public static AuthenticationResult GetS2SAccessToken(string authority, string resource, string clientId, SecureString clientSecret)
        {
            // Client credential consists of the "client" AAD web application's Client ID
            // and the key that was generated for the application in the AAD Azure portal extension.
            var clientCredential = new ClientCredential(clientId, clientSecret);

            // The authentication context represents the AAD directory.
            AuthenticationContext context = new AuthenticationContext(authority, false);

            // Fetch an access token from AAD.
            AuthenticationResult authenticationResult = context.AcquireToken(
                resource,
                clientCredential);
            return authenticationResult;
        }
    }
}