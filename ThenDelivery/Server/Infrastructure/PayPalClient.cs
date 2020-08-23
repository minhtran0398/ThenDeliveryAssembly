using System;
using PayoutsSdk.Core;
using PayPalHttp;
namespace ThenDelivery.Server.Infrastructure
{
	public class PayPalClient
	{
		/**
			 Set up PayPal environment with sandbox credentials.
			 In production, use LiveEnvironment.
		 */
		public static PayPalEnvironment environment()
		{
			string clientId = "AeapNk1TGW1kGPsG5VtXer0MnwnJv8a4gMGeaj8yjYTsO7hD9OvpImGPeaZSIkpRedGvudQhYQgAIOvL";
			string secretId = "ED2dyc0j8IPL__zN7i6t6CiYEPBM8r7TTbSF-XPAk-1bWnZcbrW5-LnPsIg_z0cAj8btjocOkBpjcca6";
			return new SandboxEnvironment(clientId, secretId);
		}
		/**
			 Returns PayPalHttpClient instance to invoke PayPal APIs.
		 */
		public static HttpClient client()
		{
			return new PayPalHttpClient(environment());
		}
		public static HttpClient client(string refreshToken)
		{
			return new PayPalHttpClient(environment(), refreshToken);
		}
	}
}