using System;
using System.Collections.Generic;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Messages;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// A decorator for <see cref="IAuthenticationResponse"/> objects.
    /// </summary>
    public class OpenIdResponse : IAuthenticationResponse
    {
        /// <summary>
        /// Gets or sets the <see cref="IAuthenticationResponse"/> decorated by this object.
        /// </summary>
        /// <value>The response.</value>
        public IAuthenticationResponse Response { get; private set; }

        /// <summary>
        /// Gets the database identifier.
        /// </summary>
        /// <value>The database identifier.</value>
        public string DatabaseIdentifier
        {
            get
            {
                return ClaimedIdentifier.ToString();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public OpenIdResponse(IAuthenticationResponse response)
        {
            Response = response;
        }

        /// <summary>
        /// Gets the Identifier that the end user claims to own.  For use with user database storage and lookup.
        /// May be null for some failed authentications (i.e. failed directed identity authentications).
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// 	<para>
        /// This is the secure identifier that should be used for database storage and lookup.
        /// It is not always friendly (i.e. =Arnott becomes =!9B72.7DD1.50A9.5CCD), but it protects
        /// user identities against spoofing and other attacks.
        /// </para>
        /// 	<para>
        /// For user-friendly identifiers to display, use the
        /// <see cref="P:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.FriendlyIdentifierForDisplay"/> property.
        /// </para>
        /// </remarks>
        public Identifier ClaimedIdentifier { get { return Response.ClaimedIdentifier; } }

        /// <summary>
        /// Gets the details regarding a failed authentication attempt, if available.
        /// This will be set if and only if <see cref="P:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.Status"/> is <see cref="F:DotNetOpenAuth.OpenId.RelyingParty.AuthenticationStatus.Failed"/>.
        /// </summary>
        /// <value></value>
        public Exception Exception { get { return Response.Exception; } }

        /// <summary>
        /// Gets a value indicating whether this instance has an Exception.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has exception; otherwise, <c>false</c>.
        /// </value>
        public bool HasException { get { return Response.Exception != null; } }

        /// <summary>
        /// Gets a user-friendly OpenID Identifier for display purposes ONLY.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// 	<para>
        /// This <i>should</i> be put through <see cref="M:System.Web.HttpUtility.HtmlEncode(System.String)"/> before
        /// sending to a browser to secure against javascript injection attacks.
        /// </para>
        /// 	<para>
        /// This property retains some aspects of the user-supplied identifier that get lost
        /// in the <see cref="P:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.ClaimedIdentifier"/>.  For example, XRIs used as user-supplied
        /// identifiers (i.e. =Arnott) become unfriendly unique strings (i.e. =!9B72.7DD1.50A9.5CCD).
        /// For display purposes, such as text on a web page that says "You're logged in as ...",
        /// this property serves to provide the =Arnott string, or whatever else is the most friendly
        /// string close to what the user originally typed in.
        /// </para>
        /// 	<para>
        /// If the user-supplied identifier is a URI, this property will be the URI after all
        /// redirects, and with the protocol and fragment trimmed off.
        /// If the user-supplied identifier is an XRI, this property will be the original XRI.
        /// If the user-supplied identifier is an OpenID Provider identifier (i.e. yahoo.com),
        /// this property will be the Claimed Identifier, with the protocol stripped if it is a URI.
        /// </para>
        /// 	<para>
        /// It is <b>very</b> important that this property <i>never</i> be used for database storage
        /// or lookup to avoid identity spoofing and other security risks.  For database storage
        /// and lookup please use the <see cref="P:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.ClaimedIdentifier"/> property.
        /// </para>
        /// </remarks>
        public string FriendlyIdentifierForDisplay { get { return Response.FriendlyIdentifierForDisplay; } }


        /// <summary>
        /// Gets information about the OpenId Provider, as advertised by the
        /// OpenID discovery documents found at the <see cref="P:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.ClaimedIdentifier"/>
        /// location, if available.
        /// </summary>
        /// <value>
        /// The Provider endpoint that issued the positive assertion;
        /// or <c>null</c> if information about the Provider is unavailable.
        /// </value>
        public IProviderEndpoint Provider { get { return Response.Provider; } }

        /// <summary>
        /// Gets the detailed success or failure status of the authentication attempt.
        /// </summary>
        /// <value></value>
        public AuthenticationStatus Status { get { return Response.Status; } }

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated { get { return Response.Status.Equals(AuthenticationStatus.Authenticated); } }

        /// <summary>
        /// Gets a callback argument's value that was previously added using
        /// <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationRequest.AddCallbackArguments(System.String,System.String)"/>.
        /// </summary>
        /// <param name="key">The name of the parameter whose value is sought.</param>
        /// <returns>
        /// The value of the argument, or null if the named parameter could not be found.
        /// </returns>
        /// <remarks>
        /// Callback parameters are only available if they are complete and untampered with
        /// since the original request message (as proven by a signature).
        /// If the relying party is operating in stateless mode <c>null</c> is always
        /// returned since the callback arguments could not be signed to protect against
        /// tampering.
        /// </remarks>
        /// <requires exception="T:System.ArgumentException">!String.IsNullOrEmpty(key)</requires>
        /// <exception cref="T:System.ArgumentException">String.IsNullOrEmpty(key)</exception>
        public string GetCallbackArgument(string key) { return Response.GetCallbackArgument(key);  }

        /// <summary>
        /// Gets all the callback arguments that were previously added using
        /// <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationRequest.AddCallbackArguments(System.String,System.String)"/> or as a natural part
        /// of the return_to URL.
        /// </summary>
        /// <returns>A name-value dictionary.  Never null.</returns>
        /// <remarks>
        /// Callback parameters are only available if they are complete and untampered with
        /// since the original request message (as proven by a signature).
        /// If the relying party is operating in stateless mode an empty dictionary is always
        /// returned since the callback arguments could not be signed to protect against
        /// tampering.
        /// </remarks>
        public IDictionary<string, string> GetCallbackArguments() { return Response.GetCallbackArguments();  }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetExtension<T>() where T : IOpenIdMessageExtension { return Response.GetExtension<T>(); }

        /// <summary>
        /// Tries to get an OpenID extension that may be present in the response.
        /// </summary>
        /// <param name="extensionType">Type of the extension to look for in the response.</param>
        /// <returns>
        /// The extension, if it is found.  Null otherwise.
        /// </returns>
        /// <remarks>
        /// 	<para>Extensions are returned only if the Provider signed them.
        /// Relying parties that do not care if the values were modified in
        /// transit should use the <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.GetUntrustedExtension(System.Type)"/> method
        /// in order to allow the Provider to not sign the extension. </para>
        /// 	<para>Unsigned extensions are completely unreliable and should be
        /// used only to prefill user forms since the user or any other third
        /// party may have tampered with the data carried by the extension.</para>
        /// 	<para>Signed extensions are only reliable if the relying party
        /// trusts the OpenID Provider that signed them.  Signing does not mean
        /// the relying party can trust the values -- it only means that the values
        /// have not been tampered with since the Provider sent the message.</para>
        /// </remarks>
        /// <requires exception="T:System.ArgumentNullException">extensionType != null</requires>
        /// <exception cref="T:System.ArgumentNullException">extensionType == null</exception>
        /// <requires exception="T:System.ArgumentException">typeof(IOpenIdMessageExtension).IsAssignableFrom(extensionType)</requires>
        /// <exception cref="T:System.ArgumentException">!(typeof(IOpenIdMessageExtension).IsAssignableFrom(extensionType))</exception>
        public IOpenIdMessageExtension GetExtension(Type extensionType) { return Response.GetExtension(extensionType); }

        /// <summary>
        /// Gets a callback argument's value that was previously added using
        /// <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationRequest.AddCallbackArguments(System.String,System.String)"/>.
        /// </summary>
        /// <param name="key">The name of the parameter whose value is sought.</param>
        /// <returns>
        /// The value of the argument, or null if the named parameter could not be found.
        /// </returns>
        /// <remarks>
        /// Callback parameters are only available even if the RP is in stateless mode,
        /// or the callback parameters are otherwise unverifiable as untampered with.
        /// Therefore, use this method only when the callback argument is not to be
        /// used to make a security-sensitive decision.
        /// </remarks>
        /// <requires exception="T:System.ArgumentException">!string.IsNullOrEmpty(key)</requires>
        /// <exception cref="T:System.ArgumentException">string.IsNullOrEmpty(key)</exception>
        public string GetUntrustedCallbackArgument(string key) { return Response.GetUntrustedCallbackArgument(key); }

        /// <summary>
        /// Gets all the callback arguments that were previously added using
        /// <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationRequest.AddCallbackArguments(System.String,System.String)"/> or as a natural part
        /// of the return_to URL.
        /// </summary>
        /// <returns>A name-value dictionary.  Never null.</returns>
        /// <remarks>
        /// Callback parameters are only available even if the RP is in stateless mode,
        /// or the callback parameters are otherwise unverifiable as untampered with.
        /// Therefore, use this method only when the callback argument is not to be
        /// used to make a security-sensitive decision.
        /// </remarks>
        /// <ensures>Contract.Result&lt;IDictionary&lt;string, string&gt;&gt;() != null</ensures>
        public IDictionary<string, string> GetUntrustedCallbackArguments() { return Response.GetUntrustedCallbackArguments(); }

        /// <summary>
        /// Gets the untrusted extension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUntrustedExtension<T>() where T : IOpenIdMessageExtension { return Response.GetUntrustedExtension<T>(); }

        /// <summary>
        /// Tries to get an OpenID extension that may be present in the response, without
        /// requiring it to be signed by the Provider.
        /// </summary>
        /// <param name="extensionType">Type of the extension to look for in the response.</param>
        /// <returns>
        /// The extension, if it is found.  Null otherwise.
        /// </returns>
        /// <remarks>
        /// 	<para>Extensions are returned whether they are signed or not.
        /// Use the <see cref="M:DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse.GetExtension(System.Type)"/> method to retrieve
        /// extension responses only if they are signed by the Provider to
        /// protect against tampering. </para>
        /// 	<para>Unsigned extensions are completely unreliable and should be
        /// used only to prefill user forms since the user or any other third
        /// party may have tampered with the data carried by the extension.</para>
        /// 	<para>Signed extensions are only reliable if the relying party
        /// trusts the OpenID Provider that signed them.  Signing does not mean
        /// the relying party can trust the values -- it only means that the values
        /// have not been tampered with since the Provider sent the message.</para>
        /// </remarks>
        /// <requires exception="T:System.ArgumentNullException">extensionType != null</requires>
        /// <exception cref="T:System.ArgumentNullException">extensionType == null</exception>
        /// <requires exception="T:System.ArgumentException">typeof(IOpenIdMessageExtension).IsAssignableFrom(extensionType)</requires>
        /// <exception cref="T:System.ArgumentException">!(typeof(IOpenIdMessageExtension).IsAssignableFrom(extensionType))</exception>
        public IOpenIdMessageExtension GetUntrustedExtension(Type extensionType) { return Response.GetUntrustedExtension(extensionType); }
    }
}
